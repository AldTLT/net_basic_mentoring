using System;

namespace Library.File
{
    /// <summary>
    /// Arguments for event
    /// </summary>
    public class FileEventArgs : EventArgs
    {
        /// <summary>
        /// Name of founded file or directory
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Flag, break of finding process
        /// </summary>
        public bool Break { get; set; }

        /// <summary>
        /// Flag, exclude file or directory from finding if true
        /// </summary>
        public bool Exclude { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">Name of founded file or directory</param>
        public FileEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}
