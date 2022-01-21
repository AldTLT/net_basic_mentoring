using System;
using System.Configuration;
using System.Diagnostics;

namespace LogAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ConfigurationManager.AppSettings;
            var logParserPath = settings["logParserPath"];
            var logFilesPath = settings["logFilesPath"];
            var arguments = settings["arguments"];
            var query1 = settings["query1"];
            var query2 = settings["query2"];

            var process = new Process();
            process.StartInfo.FileName = logParserPath;

            Console.WriteLine("Logs statistic:\n");

            //First query            
            process.StartInfo.Arguments = GetQuery(query1, logFilesPath, arguments);
            process.Start();

            //Second query
            process.StartInfo.Arguments = GetQuery(query2, logFilesPath, arguments);
            process.Start();
            Console.ReadKey();
        }

        private static string GetQuery(string query, string path, string arguments)
        {
            arguments.Replace('\'', '\"');
            return arguments.Replace("query", $"\"{query.Replace("logFilesPath", path)}\"");
        }
    }
}
