using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HttpServer
{
    class Program
    {
        private const string Key = "key";
        private const string Val = "val";
        private const string Regex = @"^\s*(?<" + Key + @">\w*)\s(?<" + Val + ">[^#]*).*$";


        private static string GetFilename(string[] args)
        {
            var (configParameter, index)  = args
                .Select((p, i) => (p, i))
                .FirstOrDefault(tuple => tuple.Item1 == "--config");

            if (configParameter == null)
            {
                // Дефолтное значение
                return "httpd.conf";
            }

            if (index >= args.Length - 1)
            {
                Console.WriteLine("invalid cli parameters");
                return null;
            }
            return args[index + 1];
        }
        
        private static ServerSettings LoadSettings(string filename)
        {
            var regex = new Regex(
                Regex,
                RegexOptions.Compiled
                | RegexOptions.Singleline);

            var settings = new Dictionary<string, string>();
            var lines = File.ReadAllLines(filename);
            foreach(var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    var k = match.Groups[Key].Value.Trim();
                    var v = match.Groups[Val].Value.Trim();
                    settings[k] = v;
                }
            }
            
            return new ServerSettings(settings);
        }
        
        
        public static void Main(string[] args)
        {
            var filename = GetFilename(args);
            if (filename == null)
            {
                return;
            } 
            var settings = LoadSettings(filename);
            if (settings != null)
            {
                new HttpServer(settings)
                    .RunServer()
                    .Wait();
            }
        }
    }
}