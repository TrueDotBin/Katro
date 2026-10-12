using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using Cosmos.Kernel.System.Input;
using Katro.GUI.Widgets;
using Katro.GUI.Windows;
using Katro.Processing;
using Katro.Processing.Processes;
using Katro.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static Png? s_wallpaper;
        private static Image? s_resizedWallpaper;

        private static void CreateTestProcesses()
        {
            var testWindowProc = new TestWindowProcess();
            var testProc = new TestProcess();

            ProcessManager.Start(testProc);
            ProcessManager.Start(testWindowProc);
        }

        private static void LoadFonts()
        {
            var interRegularBytes = EmbeddedResourceManager.GetEmbeddedResource("Katro.Resources.Fonts.InterRegular.ttf");
            FontManager.InterRegular = new TrueTypeFont(interRegularBytes);

            var interBoldBytes = EmbeddedResourceManager.GetEmbeddedResource("Katro.Resources.Fonts.InterBold.ttf");
            FontManager.InterBold = new TrueTypeFont(interBoldBytes);
        }

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

            var wallpaperBytes = EmbeddedResourceManager.GetEmbeddedResource("Katro.Resources.Images.Wallpapers.wallpaper.png");
            s_wallpaper = new Png(wallpaperBytes);

            LoadFonts();
            CreateTestProcesses();

            CurrentCanvas.DrawImage(s_wallpaper, 0, 0, CurrentCanvas.Width, CurrentCanvas.Height);
            s_resizedWallpaper = CurrentCanvas.GetImage(0, 0, CurrentCanvas.Width, CurrentCanvas.Height);

            s_isInitialized = true;
        }

        /// <summary>
        /// Draws the desktop.
        /// </summary>
        public static void Draw()
        {
            if (CurrentCanvas == null)
                return;

            if (s_resizedWallpaper == null)
                CurrentCanvas.Clear();
            else
                CurrentCanvas.DrawImage(s_resizedWallpaper, 0, 0, CurrentCanvas.Width, CurrentCanvas.Height);

            ProcessManager.Update();

            WindowManager.Update();
            WindowManager.Draw(CurrentCanvas);

            MouseCursor.Draw(CurrentCanvas);

            CurrentCanvas.Display();
        }
    }
}
