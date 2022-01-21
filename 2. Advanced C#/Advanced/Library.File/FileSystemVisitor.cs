using Library.File.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library.File
{
    /// <summary>
    /// The class allow to traverses folders from a specified point
    /// </summary>
    public class FileSystemVisitor
    {
        /// <summary>
        /// Path to folder to start of file system visitor
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Predicate to filter files and directories
        /// </summary>
        private readonly Predicate<string> _predicate;

        /// <summary>
        /// Flag - the first visit of directory
        /// </summary>
        private bool _firstVisitFlag = true;

        /// <summary>
        /// Event Start of the visitor
        /// </summary>
        public event EventHandler<ProcessEventArgs> Start;

        /// <summary>
        /// Event Stop of the visitor
        /// </summary>
        public event EventHandler<ProcessEventArgs> Stop;

        /// <summary>
        /// Event File was found
        /// </summary>
        public event EventHandler<FileEventArgs> FileFinded;

        /// <summary>
        /// Event Directory was found
        /// </summary>
        public event EventHandler<FileEventArgs> DirectoryFinded;

        /// <summary>
        /// Event Filtered file was found
        /// </summary>
        public event EventHandler<FileEventArgs> FilteredFileFinded;

        /// <summary>
        /// Event Filtered directory was found
        /// </summary>
        public event EventHandler<FileEventArgs> FilteredDirectoryFinded;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path from visit</param>
        public FileSystemVisitor(string path)
        {
            _path = path;
            _predicate = (name) => true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path from visit</param>
        /// <param name="predicate">Predicate to filter founded files and directories</param>
        public FileSystemVisitor(string path, Predicate<string> predicate)
        {
            _path = path;
            _predicate = predicate;
        }

        /// <summary>
        /// The method returns IEnumerable contains files and directories.
        /// </summary>
        /// <returns>IEnumerable of files and directories</returns>
        public IEnumerable<string> GetFilesAndDirectories()
        {
            var filesAndDirectories = GetFilesAndDirRecursive(_path, "");
            return filesAndDirectories;
        }

        /// <summary>
        /// The method recursively traverses folders from a specified point
        /// </summary>
        /// <param name="path">Path to specified point</param>
        /// <param name="parentDirectories">Path from specified point to current file or directory</param>
        /// <returns></returns>
        private IEnumerable<string> GetFilesAndDirRecursive(string path, string parentDirectories)
        {
            var directoryName = Path.GetFileName(path);
            var parentPath = Path.Combine(parentDirectories);
            var currentDirectory = _firstVisitFlag ? "" : Path.Combine(parentPath, $"{directoryName}\\");

            if (!_firstVisitFlag)
            {
                var directoryArgs = new FileEventArgs(directoryName);
                DirectoryFinded?.Invoke(this, directoryArgs);

                // Break process
                if (directoryArgs.Break)
                {
                    yield break;
                }

                if (_predicate(directoryName) && !directoryArgs.Exclude)
                {
                    FilteredDirectoryFinded?.Invoke(this, directoryArgs);
                    yield return currentDirectory;

                    // Break process
                    if (directoryArgs.Break)
                    {
                        yield break;
                    }
                }
            } 
            else
            {
                var start = new ProcessEventArgs(Process.Start, _path);
                Start?.Invoke(this, start);
            }

            _firstVisitFlag = false;
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileArgs = new FileEventArgs(fileName);
                FileFinded?.Invoke(this, fileArgs);

                // Break process
                if (fileArgs.Break)
                {
                    yield break;
                }

                if (_predicate(fileName) && !fileArgs.Exclude)
                {
                    FilteredFileFinded?.Invoke(this, fileArgs);
                    yield return Path.Combine(currentDirectory, fileName);

                    // Break process
                    if (fileArgs.Break)
                    {
                        yield break;
                    }
                }
            }

            foreach (var directory in directories)
            {
                foreach (var filesInDirectory in GetFilesAndDirRecursive(directory, currentDirectory))
                {
                    yield return filesInDirectory;
                }
            }

            // If this is first call
            if (_path.Equals(path))
            {
                Stop?.Invoke(this, new ProcessEventArgs(Process.Stop, _path));
            }
        }
    }
}
