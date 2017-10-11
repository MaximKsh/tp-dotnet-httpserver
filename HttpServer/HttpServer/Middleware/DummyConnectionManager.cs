namespace HttpServer.Middleware
{
    public class DummyConnectionManager: IHttpServerMiddleware
    {
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            response.KeepAlive = false;
            response.Headers["Connection"] = "close";
        }
        #endregion
    }
}