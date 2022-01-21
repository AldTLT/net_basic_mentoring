using ConsoleApp.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ConsoleApp.Resources;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp
{
    public class FilePerformer : IDisposable
    {
        /// <summary>
        /// Name of default folder
        /// </summary>
        private const string DefaultFolderName = "default";

        /// <summary>
        /// Format of a date for postfix
        /// </summary>
        private const string PostfixFormat = "_ddMMyyyy";

        /// <summary>
        /// Max number of retries to transfer file
        /// </summary>
        private const int MaxRetryNumber = 10;

        /// <summary>
        /// Path to a directory
        /// </summary>
        private readonly string _rootPath;

        /// <summary>
        /// Collection of FileSystemWatchers
        /// </summary>
        private readonly ICollection<FileSystemWatcher> _fileSystemWatchers;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly CustomConfigurationSection _config;

        /// <summary>
        /// Current culture
        /// </summary>
        private readonly CultureInfo _culture;

        /// <summary>
        /// Index for files
        /// </summary>
        private int _index;

        public FilePerformer(string rootPath, CustomConfigurationSection config)
        {
            _rootPath = rootPath;
            _config = config;
            _fileSystemWatchers = new List<FileSystemWatcher>();
            _index = 1;
            _culture = config?.culture?.Culture;
            Messages.Culture = _culture;
        }

        /// <summary>
        /// Perform of the watcher
        /// </summary>
        public void Perform()
        {
            Console.WriteLine(Messages.RootFolder, _rootPath);
            var folders = _config.Folders;

            Console.WriteLine(Messages.Start);
            if (folders == null)
            {
                Console.WriteLine(Messages.NoFolders);
                return;
            }

            foreach (var folder in folders)
            {
                var folderName = ((FolderElement)folder).Name;
                Console.WriteLine(Messages.Folder, folderName);
                var fullPath = Path.Combine(_rootPath, folderName);

                if (!Directory.Exists(fullPath))
                {
                    Console.WriteLine(Messages.FolderNotExists, fullPath);

                    try
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(Messages.CreateFolderException, fullPath, e.Message);
                        continue;
                    }
                }

                var fileWatcher = new FileSystemWatcher(fullPath);
                _fileSystemWatchers.Add(fileWatcher);

                fileWatcher.Filter = "";
                fileWatcher.Created += OnCreate;
                fileWatcher.EnableRaisingEvents = true;
            }
        }

        /// <summary>
        /// OnCreate handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnCreate(object sender, FileSystemEventArgs args)
        {
            var fileInfo = new FileInfo(args.FullPath);

            // Check if it's not directory
            if (!Directory.Exists(args.FullPath))
            {
                var fileName = args.Name;
                var fileWatcher = sender as FileSystemWatcher;

                Console.WriteLine(Messages.FileFound, fileName, fileWatcher.Path, fileInfo.CreationTime.ToString("d", _culture));

                var rule = GetRule(fileName);
                TransferFile(fileName, fileWatcher.Path, rule);
            }
        }

        /// <summary>
        /// The method returns RuleElement if pattern is match of file name
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>RuleElement, or null if rule not found</returns>
        private RuleElement GetRule(string fileName)
        {
            foreach (var ruleObject in _config.Rules)
            {
                var rule = (RuleElement)ruleObject;
                var pattern = rule.Pattern;

                if (Regex.IsMatch(fileName, pattern))
                {
                    Console.WriteLine(Messages.RuleFound, pattern, fileName);
                    return rule;
                }
            }

            Console.WriteLine(Messages.RuleNotFound, fileName);
            return null;
        }

        /// <summary>
        /// Transfer file according a rule
        /// </summary>
        /// <param name="fileName">File to transfer</param>
        /// <param name="pathFrom">Path to the file</param>
        /// <param name="rule">Rule of transfer</param>
        private void TransferFile(string fileName, string pathFrom, RuleElement rule)
        {
            var prefix = string.Empty;
            var postfix = string.Empty;

            if (rule != null)
            {
                Console.WriteLine(Messages.SequenceNumber, rule.AddSequence);
                Console.WriteLine(Messages.Date, rule.AddDate);

                if (rule.AddSequence)
                {
                    prefix = $"{_index}_";
                    _index++;
                }

                postfix = rule.AddDate ? DateTime.Now.ToString(PostfixFormat) : string.Empty;
            }

            var pathTo = rule == null ? Path.Combine(pathFrom, DefaultFolderName) : Path.Combine(pathFrom, rule.FolderName);
            var fullPathFrom = Path.Combine(pathFrom, fileName);
            var fileWithoutExt = Path.GetFileNameWithoutExtension(fullPathFrom);
            var extension = Path.GetExtension(fullPathFrom);
            var fullPathTo = Path.Combine(pathTo, $"{prefix}{fileWithoutExt}{postfix}{extension}");
            var retryIndex = 0;

            do
            {
                try
                {
                    Console.WriteLine(Messages.TransferFile, fileName, fullPathTo);
                    if (!Directory.Exists(pathTo))
                    {
                        Directory.CreateDirectory(pathTo);
                    }

                    var fileInfo = new FileInfo(fullPathFrom);
                    fileInfo.MoveTo(fullPathTo, true);

                    Console.WriteLine(Messages.Successful);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine(Messages.TransferFileException);
                    Thread.Sleep(1000);
                    retryIndex++;
                }
            } while (retryIndex < MaxRetryNumber);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (var fileSystemWatcher in _fileSystemWatchers)
            {
                fileSystemWatcher.Dispose();
            }
        }
    }
}
