using Cosmos.Kernel.System.Graphics;
using Katro.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Processing
{
    /// <summary>
    /// Represents a process.
    /// </summary>
    public abstract class Process
    {
        /// <summary>
        /// The name of this process.
        /// </summary>
        public abstract string? Name { get; set; }

        /// <summary>
        /// The window of this process, null if this is a window-less process.
        /// </summary>
        public virtual Window? Window { get; set; }

        /// <summary>
        /// Updates this process.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Called whenever this process has been closed.
        /// </summary>
        public virtual void OnStart()
        {
            if (Window != null)
                WindowManager.Windows.Add(Window);
        }

        /// <summary>
        /// Called whenever this process has been closed.
        /// </summary>
        public virtual void OnQuit()
        {
            if (Window != null)
                WindowManager.Windows.Remove(Window);
        }
    }
}
