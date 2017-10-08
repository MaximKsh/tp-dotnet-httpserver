namespace HttpServer.Middleware
{
    public interface IHttpServerMiddleware
    {
        void Invoke(HttpRequest request, HttpResponse response);
    }
}