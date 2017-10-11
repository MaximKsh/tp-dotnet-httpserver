using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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

        private const int SendTimeoutMsPerKb = 5_000;
        
        private const int ReceiveTimeoutMs = 5_000;

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
                //new ConnectionManager(),
                new DummyConnectionManager(),
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
            var connectionId = Guid.NewGuid().ToString();
            try
            {
                Log(connectionId, "begin request");
                using (var stream = tcpClient.GetStream())
                {
                    var keepConnection = false;
                    do
                    {
                        var stopWatch = new Stopwatch();
                        stopWatch.Start();

                        var rawContent = await ReadRequest(stream);

                        LogRequest(connectionId, rawContent);
                        
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
                                Log(connectionId, $" in middleware {m.GetType().FullName}");
                                throw;
                            }
                        }

                        LogResponse(connectionId, response.RawHeadersResponse);
                        
                        await SendResponse(
                            stream,
                            response.RawHeadersResponse,
                            response.ResponseContentFilePath,
                            response.ContentLength);

                        stopWatch.Stop();
                        Log(connectionId, $"response complete in {stopWatch.ElapsedMilliseconds} ms");

                        keepConnection = response.KeepAlive;
                    } while (keepConnection);
                }
            }
            catch (Exception e)
            {
                Log(connectionId, $"Exception occured {e.GetType().FullName} \n {e.Message}\n{e.StackTrace}");
            }
            finally
            {
                try
                {
                    Log(connectionId, "connection close");
                }
                catch (Exception e)
                {
                    Log(connectionId, $"Exception occured {e.GetType().FullName} \n {e.Message}\n{e.StackTrace}");
                }
            }
        }

        private static async Task<string> ReadRequest(NetworkStream stream)
        {
            var buf = new byte[BufferSize];
            var builder = new StringBuilder(InitialBuilderCapacity);
            
            using (var cancellationTokenSource = new CancellationTokenSource(ReceiveTimeoutMs))
            using (cancellationTokenSource.Token.Register(stream.Close))
            {
                do
                {
                    var readpos = 0;
                    do
                    {
                        readpos += await stream.ReadAsync(buf, readpos, BufferSize - readpos, cancellationTokenSource.Token);
                    } while (readpos < BufferSize && stream.DataAvailable);
                    builder.Append(Encoding.UTF8.GetString(buf, 0, readpos));
                } while (stream.DataAvailable);
            }
            
            return builder.ToString();
        }

        private static async Task SendResponse(
            NetworkStream stream,
            string head,
            string contentFilename,
            long contentLength)
        {
            var timeout = SendTimeoutMsPerKb;
            if (contentFilename != null && contentLength > 0)
            {
                timeout *= ((int) Math.Min(contentLength, int.MaxValue) / 1024) + 1;
            }
            using (var cancellationTokenSource = new CancellationTokenSource(timeout))
            using (cancellationTokenSource.Token.Register(stream.Close))
            {
                var bytes = Encoding.UTF8.GetBytes(head);
                await stream.WriteAsync(bytes, 0, bytes.Length, cancellationTokenSource.Token);
            
                if (contentFilename != null)
                {
                    using (var fileStream = new FileStream(contentFilename, FileMode.Open, FileAccess.Read))
                    {
                        await fileStream.CopyToAsync(stream, 81920, cancellationTokenSource.Token);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Log(string id, string text)
        {
             Console.WriteLine($"[{id}]: {text}.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogRequest(string id, string content)
        {
            // Console.WriteLine($"[{id}]: Request:\n{content}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void LogResponse(string id, string response)
        {
            // Console.WriteLine($"[{id}]: Response headers:\n{response}");
        }

    }
}