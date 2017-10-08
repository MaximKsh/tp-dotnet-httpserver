using System;

namespace HttpServer
{
    public class ServerSettings
    {
        public ServerSettings(
            string directoryRoot,
            string defaultUrl)
        {
            this.DirectoryRoot = directoryRoot ?? throw new ArgumentNullException();
            this.DefaultUrl = defaultUrl ?? throw new ArgumentNullException();
        }
        
        public string DirectoryRoot { get; }
        
        public string DefaultUrl { get; }
    }
}