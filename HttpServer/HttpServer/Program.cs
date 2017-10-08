using System;

namespace HttpServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            new HttpServer()
                .RunServer(5001)
                .Wait();
        }
    }
}