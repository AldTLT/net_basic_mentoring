using Library.File;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var path = Directory.GetCurrentDirectory();

            // Print files and directories
            PrintFinding(path);

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();

            // Filter files using predicate - print only .dll. Also demonstrate events.
            FilterFiles(path, (fileName) => fileName.Contains(".dll"));

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();

            // Print files and directories with start/stop event
            PrintFindingStartStop(path);

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();

            // Break process after ConsoleApp.exe finding
            BreakFinding(path);

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();

            // Just FileFinded and DirectoryFinded demonstrate
            PrintFilesAndDirWithEvents(path);

            Console.WriteLine("\nPress any key");
            Console.ReadKey();
            Console.Clear();

            // Exclude files .exe using event
            ExcludeFilesEvent(path);

            Console.ReadKey();
        }

        /// <summary>
        /// Find files and directories
        /// </summary>
        /// <param name="path">Root path</param>
        private static void PrintFinding(string path)
        {
            var fileSystemVisitor = new FileSystemVisitor(path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            Console.WriteLine("Files and directories:\n");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Print only .dll files. Demostrate FilteredFileFinded event
        /// </summary>
        /// <param name="path">Root path</param>
        private static void FilterFiles(string path, Predicate<string> predicate)
        {
            var fileSystemVisitor = new FileSystemVisitor(path, predicate);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Subscribe to event
            fileSystemVisitor.FilteredFileFinded += (sender, args) => Console.WriteLine($"Filtered file finded: {args.FileName}");

            Console.WriteLine("Filter - print only dll files:\n");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Find files and directories with start and stop events
        /// </summary>
        /// <param name="path">Root path</param>
        private static void PrintFindingStartStop(string path)
        {
            var fileSystemVisitor = new FileSystemVisitor(path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Subscribe to events
            fileSystemVisitor.Start += (sender, args) => Console.WriteLine("Start process\n");
            fileSystemVisitor.Stop += (sender, args) => Console.WriteLine("\nStop process");

            Console.WriteLine("Start and Stop events:\n");

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Break finding after ConsoleApp.exe finding
        /// </summary>
        /// <param name="path">Root path</param>
        private static void BreakFinding(string path)
        {
            var fileSystemVisitor = new FileSystemVisitor(path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            Console.WriteLine("Find file ConsoleApp.exe and break the process:\n");

            // Subscribe to event
            fileSystemVisitor.FileFinded += (sender, args) =>
            {
                if (args.FileName.Equals("ConsoleApp.exe"))
                {
                    Console.WriteLine(args.FileName);
                    Console.WriteLine("File ConsoleApp.exe found. Break the process.");
                    args.Break = true;
                }
            };

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Demonstrate FileFinded and DirectoryFinded events
        /// </summary>
        /// <param name="path">Root path</param>
        private static void PrintFilesAndDirWithEvents(string path)
        {
            var fileSystemVisitor = new FileSystemVisitor(path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Subscribe to events
            fileSystemVisitor.FileFinded += (sender, args) => Console.WriteLine($"File finded event: {args.FileName}");
            fileSystemVisitor.DirectoryFinded += (sender, args) => Console.WriteLine($"Directory finded event: {args.FileName}");

            Console.WriteLine("FileFinded and DirectoryFinded events:\n");

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        /// <summary>
        /// Filter files using event - exclude .exe files
        /// </summary>
        /// <param name="path">Root path</param>
        private static void ExcludeFilesEvent(string path)
        {
            var fileSystemVisitor = new FileSystemVisitor(path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            Console.WriteLine("Exclude exe files using events:\n");

            // Subscribe to events
            fileSystemVisitor.FileFinded += (sender, args) =>
            {
                if (args.FileName.Contains(".exe"))
                {
                    args.Exclude = true;
                }
            };

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}
