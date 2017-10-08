using System;

namespace HttpServer
{
    public enum HttpStatusCode
    {
        Ok,
        BadRequest,
        NotFound,
        NotAllowed,
        InternalServerError,
    }

    public static class HttpStatusCodeExtensions
    {
        public static string GetCaption(this HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Ok:
                    return "200 OK";
                case HttpStatusCode.BadRequest:
                    return "400 Bad Request";
                case HttpStatusCode.NotFound:
                    return "404 Not Found";
                case HttpStatusCode.NotAllowed:
                    return "405 Method Not Allowed";
                case HttpStatusCode.InternalServerError:
                    return "500 Internal Server Error";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    
}