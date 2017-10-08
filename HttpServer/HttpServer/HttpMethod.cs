
using System;

namespace HttpServer
{
    public enum HttpMethod
    {
        Get,
        Head,
    }

    public static class HttpMethodExtensions
    {
        public static string GetCaption(this HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Get:
                    return "GET";
                case HttpMethod.Head:
                    return "HEAD";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
}