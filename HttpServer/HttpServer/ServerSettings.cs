using System;
using System.Collections.Generic;
using System.IO;

namespace HttpServer
{
    public class ServerSettings
    {
        public ServerSettings(
            string directoryRoot,
            string defaultUrl,
            short port,
            short threadLimit)
        {
            this.DocumentRoot = directoryRoot ?? throw new ArgumentNullException();
            this.DefaultDirectioryFile = defaultUrl ?? throw new ArgumentNullException();
            this.Port = port;
            this.ThreadLimit = threadLimit;
        }

        public ServerSettings(IReadOnlyDictionary<string, string> settings)
        {
            if (settings.TryGetValue("listen", out var portStr)
                && short.TryParse(portStr, out var port))
            {
                this.Port = port;
            }

            if (settings.ContainsKey("cpu_limit"))
            {
                Console.WriteLine("cpu_limit not supported. Use thread_limit instread.");
            }
            
            if (settings.TryGetValue("thread_limit", out var threadLimitStr)
                && short.TryParse(threadLimitStr, out var threadLimit))
            {
                this.ThreadLimit = threadLimit;
            }
            
            if (settings.TryGetValue("document_root", out var documentRoot))
            {
                if (documentRoot.EndsWith("/"))
                {
                    documentRoot = documentRoot.Substring(0, documentRoot.Length - 1);
                }
                DirectoryInfo info;
                try
                {
                    info = new DirectoryInfo(documentRoot);
                }
                catch (Exception)
                {
                    info = null;
                }
                if (info == null
                    || !info.Exists)
                {
                    Console.WriteLine("Invalid document root");
                }
                else
                {
                    this.DocumentRoot = documentRoot;
                }
            }
        }

        public string DocumentRoot { get; } = "/var/www";

        public string DefaultDirectioryFile { get; } = "index.html";

        public short Port { get; } = 80;

        public short ThreadLimit { get; } = 0;
    }
}