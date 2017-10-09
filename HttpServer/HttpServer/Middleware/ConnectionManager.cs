namespace HttpServer.Middleware
{
    public class ConnectionManager: IHttpServerMiddleware
    {
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            
            if (KeepAlive11(request)
                || KeepAlive10(request))
            {
                response.KeepAlive = true;
                response.Headers["Connection"] = "keep-alive";
            }
            else
            {
                response.KeepAlive = false;
                response.Headers["Connection"] = "close";
            }
        }

        private static bool KeepAlive11(HttpRequest request) =>
            request.HttpVersion == HttpVersion.Http11
            && (!request.Headers.TryGetValue("Connection", out var requestConnection)
                || requestConnection == "keep-alive");
        
        private static bool KeepAlive10(HttpRequest request) =>
            request.HttpVersion == HttpVersion.Http10
            && request.Headers.TryGetValue("Connection", out var requestConnection)
            && requestConnection == "keep-alive";

        #endregion
    }
}