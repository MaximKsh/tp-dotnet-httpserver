using System;
using System.Collections.Generic;
using System.IO;

namespace HttpServer.Middleware
{
    public class ContentSearch: IHttpServerMiddleware
    {
        #region fields
        
        private static readonly Dictionary<string, string> PossibleContentTypes = 
            new Dictionary<string, string>
            {
                [".html"] = "text/html",
                [".css"] = "text/css",
                [".txt"] = "text/plain",
                [".js"] = "application/javascript",
                [".jpg"] = "image/jpeg", 
                [".jpeg"] = "image/jpeg",
                [".png"] = "image/png", 
                [".gif"] = "image/gif",
                [".swf"] = "application/x-shockwave-flash",
            };
        
        private readonly ServerSettings settings;
        
        #endregion
        
        #region constuctor
        
        public ContentSearch(ServerSettings settings)
        {
            this.settings = settings;
        }
        
        #endregion
        
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            if (!response.Success)
            {
                return;
            }
            
            var path = request.Url;
            if (string.IsNullOrWhiteSpace(path))
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return;
            }
            
            if (path == "/")
            {
                path = this.settings.DefaultUrl;
            }
            else if (path[path.Length - 1] == '/')
            {
                path = path + this.settings.DefaultUrl;
            }
            if (path[0] == '/')
            {
                path = path.Substring(1);
            }
            
            var absolutePath = Path.Combine(this.settings.DirectoryRoot, path);
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(absolutePath);
            }
            catch (Exception)
            {
                fileInfo = null;
            }
            
            if (fileInfo == null
                || !fileInfo.Exists
                || !fileInfo.FullName.StartsWith(this.settings.DirectoryRoot))
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return;
            }

            response.HttpStatusCode = HttpStatusCode.Ok;
            response.Headers["Content-Length"] = fileInfo.Length.ToString();
            if (PossibleContentTypes.TryGetValue(fileInfo.Extension, out var ct))
            {
                response.Headers["Content-Type"] = ct;
            }
            if (Equals(request.HttpMethod, HttpMethod.Get))
            {
                response.ResponseContentFilePath = fileInfo.FullName;
            }
        }
        
        #endregion
    }
}