using Library.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class FileSystemVisitor_Tests
    {
        /// <summary>
        /// Path to current directory
        /// </summary>
        private string _path;

        /// <summary>
        /// Initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _path = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Test of filter predicate
        /// </summary>
        [TestMethod]
        public void FilterFiles_Test()
        {
            // Before filter
            var fileSystemVisitor = new FileSystemVisitor(_path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            Assert.IsTrue(files.Any(fileName => fileName.Contains("dll")));

            // Filter - without files and directories contains dll.
            var filteredFileSystemVisitor = new FileSystemVisitor(_path, fileName => !fileName.Contains("dll"));
            var filteredFiles = filteredFileSystemVisitor.GetFilesAndDirectories();

            Assert.IsFalse(filteredFiles.Any(fileName => fileName.Contains("dll")));
        }

        /// <summary>
        /// Test of exclude file from result using event
        /// </summary>
        [TestMethod]
        public void ExcludeFileEvent_Test()
        {
            var fileSystemVisitor = new FileSystemVisitor(_path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Result contains UnitTests.dll file
            Assert.IsTrue(files.Any(fileName => fileName.Equals("UnitTests.dll")));

            // Exclude by event
            fileSystemVisitor.FileFinded += (sender, args) =>
            {
                if (args.FileName.Equals("UnitTests.dll"))
                {
                    args.Exclude = true;
                };
            };

            var excludeFiles = fileSystemVisitor.GetFilesAndDirectories();

            // Result not contains UnitTests.dll file
            Assert.IsFalse(excludeFiles.Any(fileName => fileName.Equals("UnitTests.dll")));
        }

        /// <summary>
        /// Test of exclude directory from result using event
        /// </summary>
        [TestMethod]
        public void ExcludeDirectoryEvent_Test()
        {
            var fileSystemVisitor = new FileSystemVisitor(_path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Result contains ru folder
            Assert.IsTrue(files.Any(fileName => fileName.Equals("ru\\")));

            // Exclude by event
            fileSystemVisitor.DirectoryFinded += (sender, args) =>
            {
                if (args.FileName.Equals("ru\\"))
                {
                    args.Exclude = true;
                };
            };

            var excludeFiles = fileSystemVisitor.GetFilesAndDirectories();

            // Result not contains ru folder
            Assert.IsFalse(excludeFiles.Any(fileName => fileName.Equals("ru\\")));
        }

        /// <summary>
        /// Test of break process
        /// </summary>
        [TestMethod]
        public void StopProcess_Test()
        {
            var fileSystemVisitor = new FileSystemVisitor(_path);
            var files = fileSystemVisitor.GetFilesAndDirectories();

            // Files contains elements
            Assert.IsTrue(files.Count() > 0);

            // Break the process
            fileSystemVisitor.DirectoryFinded += (sender, args) => args.Break = true;
            files = fileSystemVisitor.GetFilesAndDirectories();

            // Files not contains any elements. Break of the process.
            Assert.IsTrue(files.Count() == 0);
        }
    }
}
