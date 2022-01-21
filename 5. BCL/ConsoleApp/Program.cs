using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using ConsoleApp.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = (CustomConfigurationSection)ConfigurationManager.GetSection("customConfigurationSection");
            var rootFolder = Directory.GetCurrentDirectory();

            using var filePerformer = new FilePerformer(rootFolder, configuration);
            filePerformer.Perform();

            Console.ReadKey();
        }
    }
}
