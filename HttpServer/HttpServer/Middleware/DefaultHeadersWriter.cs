using System;

namespace HttpServer.Middleware
{
    public class DefaultHeadersWriter: IHttpServerMiddleware
    {
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            response.Headers["Server"] = "http-dotnet-server";
            response.Headers["Date"] = DateTime.UtcNow.ToString("r");
        }
        
        #endregion
    }
}