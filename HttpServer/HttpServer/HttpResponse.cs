using System;
using System.Collections.Generic;

namespace HttpServer
{
    public class HttpResponse
    {
        private bool sucess = true;

        public bool Success
        {
            get => this.sucess;
            set
            {
                if (value)
                {
                    throw new InvalidOperationException();
                }
                this.sucess = false;
            }
        }
        
        public HttpVersion? HttpVersion { get; set; }
        
        public HttpStatusCode? HttpStatusCode { get; set; }
        
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        
        public string RawHeadersResponse { get; set; }
        
        public string ResponseContentFilePath { get; set; }
        
        public bool UseCrLf { get; set; } = false;

        public bool KeepAlive { get; set; } = false;
        
        public long ContentLength { get; set; }
    }
}