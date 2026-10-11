using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.GUI
{
    /// <summary>
    /// The Katro desktop.
    /// </summary>
    public static class Desktop
    {
        /// <summary>
        /// The canvas.
        /// </summary>
        public static Canvas? CurrentCanvas { get; private set; }

        /// <summary>
        /// Whether the desktop is running.
        /// </summary>
        public static bool IsRunning { get; private set; } = false;

        private static bool s_isInitialized = false;

        /// <summary>
        /// Initializes the desktop and marks it as running.
        /// </summary>
        public static void Start()
        {
            Init();
            IsRunning = true;
        }

        /// <summary>
        /// Marks the desktop as not running.
        /// </summary>
        public static void Stop()
        {
            IsRunning = false;
        }

        /// <summary>
        /// Initializes the desktop.
        /// </summary>
        public static void Init()
        {
            if (s_isInitialized)
                return;

            CurrentCanvas = Canvas.GetFullScreen();
            MouseCursor.SetDefaultImage();

            MouseManager.SetScreenSize(CurrentCanvas.Width, CurrentCanvas.Height);
            MouseManager.SetPosition(CurrentCanvas.Width / 2, CurrentCanvas.Height / 2);

            s_isInitialized = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Draw()
        {
            if (CurrentCanvas == null)
                return;

            CurrentCanvas.Clear();

            MouseCursor.Draw(CurrentCanvas); 

            CurrentCanvas.Display();
        }
    }
}
