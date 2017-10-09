using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpServer.Middleware;

namespace HttpServer
{
    public class HttpServer
    {
        private const int BufferSize = 1024;

        private const int InitialBuilderCapacity = 32 * BufferSize;

        private const int SendTimeoutMs = 10_000;
        
        private const int ReceiveTimeoutMs = 10_000;

        private readonly List<IHttpServerMiddleware> middleware;

        private readonly ServerSettings settings;
        
        public HttpServer(ServerSettings settings)
        {
            this.settings = settings;
            
            this.middleware = new List<IHttpServerMiddleware>
            {
                new RequestParser(),
                new ContentSearch(settings),
                new DefaultHeadersWriter(),
                new ConnectionManager(),
                new ResponseWriter(),
            };
        }
        
        public async Task RunServer()
        {
            Console.WriteLine("Server started.");
            Console.WriteLine($"Port: {this.settings.Port}");
            Console.WriteLine($"DocumentRoot: {this.settings.DocumentRoot}");
            if (this.settings.ThreadLimit > 0)
            {
                Console.WriteLine($"ThreadPool.SetMaxThreads({this.settings.ThreadLimit}, {this.settings.ThreadLimit})");
                ThreadPool.SetMaxThreads(this.settings.ThreadLimit, this.settings.ThreadLimit);
            }
            var tcpListener = TcpListener.Create(this.settings.Port);
            tcpListener.Start();
            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                this.StartProcessConnect(tcpClient); 
            }
        }

        private async Task StartProcessConnect(TcpClient tcpClient)
        {
            tcpClient.SendTimeout = SendTimeoutMs;
            tcpClient.ReceiveTimeout = ReceiveTimeoutMs;

            var connectionId = Guid.NewGuid().ToString();
            try
            {
                Console.WriteLine($"[{connectionId}]: begin request");
                using (var stream = tcpClient.GetStream())
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    var rawContent = await ReadRequest(stream);

                    var request = new HttpRequest(rawContent);
                    var response = new HttpResponse();
                    foreach (var m in this.middleware)
                    {
                        try
                        {
                            m.Invoke(request, response);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"[{connectionId}]: {m.GetType().FullName}");
                            throw;
                        }
                    }

                    await SendResponse(
                        stream,
                        response.RawHeadersResponse,
                        response.ResponseContentFilePath);

                    stopWatch.Stop();
                    Console.WriteLine($"[{connectionId}]: response complete in {stopWatch.ElapsedMilliseconds} ms");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{connectionId}]: Exception occured {e.Message}\n{e.StackTrace}");
            }
            finally
            {
                try
                {
                    tcpClient.Close();
                    Console.WriteLine($"[{connectionId}]: connection close");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[{connectionId}]: Exception occured {e.Message}\n{e.StackTrace}");
                }
            }
        }

        private static async Task<string> ReadRequest(NetworkStream stream)
        {
            var buf = new byte[BufferSize];
            var builder = new StringBuilder(InitialBuilderCapacity);
            
            do
            {
                var readpos = 0;
                do
                {
                    readpos += await stream.ReadAsync(buf, readpos, BufferSize - readpos);
                } while (readpos < BufferSize && stream.DataAvailable);
                builder.Append(Encoding.UTF8.GetString(buf, 0, readpos));
            } while (stream.DataAvailable);
            
            return builder.ToString();
        }

        private static async Task SendResponse(
            NetworkStream stream,
            string head,
            string contentFilename)
        {
            var bytes = Encoding.UTF8.GetBytes(head);
            await stream.WriteAsync(bytes, 0, bytes.Length);
            
            if (contentFilename != null)
            {
                using (var fileStream = new FileStream(contentFilename, FileMode.Open))
                {
                    await fileStream.CopyToAsync(stream);
                }
            }
        }

        
        private static void LogRequest(string id, string content)
        {
            Console.WriteLine($"[{id}]: Request:\n{content}");
        }

        private static void LogResponse(string id, string response)
        {
            Console.WriteLine($"[{id}]: Response headers:\n{response}");
        }

    }
}