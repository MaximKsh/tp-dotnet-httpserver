using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
        
        public HttpServer()
        {
            var settings = new ServerSettings(
                "/home/maxim/http-test-suite",
                "index.html");
            
            this.middleware = new List<IHttpServerMiddleware>
            {
                new HttpRequestParser(),
                new ContentSearch(settings),
                new DefaultHeadersWriter(),
                new ConnectionManager(),
                new ResponseWriter(),
            };
        }
        
        public async Task RunServer(short port)
        {
            var tcpListener = TcpListener.Create(port);
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

            using (var stream = tcpClient.GetStream())
            {
                var rawContent = await ReadRequest(stream);

                var request = new HttpRequest(rawContent);
                var response = new HttpResponse();
                foreach (var m in this.middleware)
                {
                    m.Invoke(request, response);
                }

                await SendResponse(
                    stream,
                    response.RawHeadersResponse, 
                    response.ResponseContentFilePath);
            }
            tcpClient.Close();
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

    }
}