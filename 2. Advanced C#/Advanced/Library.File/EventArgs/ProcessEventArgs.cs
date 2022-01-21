using Library.File.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.File
{
    /// <summary>
    /// Arguments for event
    /// </summary>
    public class ProcessEventArgs
    {
        /// <summary>
        /// Name of founded file or directory
        /// </summary>
        public Process ProcessStep { get; private set; }

        /// <summary>
        /// Path to specified point
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="processStep">Step of the process</param>
        public ProcessEventArgs(Process processStep, string path)
        {
            ProcessStep = processStep;
            Path = path;
        }
    }
}
