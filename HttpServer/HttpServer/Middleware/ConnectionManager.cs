namespace HttpServer.Middleware
{
    public class ConnectionManager: IHttpServerMiddleware
    {
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            // TODO: сделать с pipeline 
            response.Headers["Connection"] = "close";
        }
        
        #endregion
    }
}