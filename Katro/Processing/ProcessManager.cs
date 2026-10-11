using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Processing
{
    /// <summary>
    /// Manages all the processes.
    /// </summary>
    public static class ProcessManager
    {
        /// <summary>
        /// The list of all processes.
        /// </summary>
        public static List<Process> Processes { get; set; } = [];

        /// <summary>
        /// Starts a process.
        /// </summary>
        /// <param name="process">The process to start.</param>
        public static void Start(Process process)
        {
            Processes.Add(process);
            process.OnStart();
        }

        /// <summary>
        /// Stops a process.
        /// </summary>
        /// <param name="process">The process to stop.</param>
        public static void Quit(Process process)
        {
            Processes.Add(process);
            process.OnQuit();
        }

        /// <summary>
        /// Updates all processes.
        /// </summary>
        public static void Update()
        {
            foreach (var proc in Processes)
                proc.Update();
        }
    }
}
