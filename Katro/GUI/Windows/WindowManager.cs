using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.GUI.Windows
{
    /// <summary>
    /// Manages all windows.
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// The list of all open windows.
        /// </summary>
        public static List<Window> Windows { get; set; } = [];

        /// <summary>
        /// The title font.
        /// </summary>
        public static readonly Font? TitleFont = FontManager.InterBold;

        /// <summary>
        /// The window title margin.
        /// </summary>
        public const int WindowTitleMargin = 5;

        private static void DrawWindow(Canvas canvas, Window win)
        {
            canvas.DrawFilledRectangle(Colors.Base, win.X, win.Y, win.Width, win.Height);

            var titleX = win.X + WindowTitleMargin;
            var titleY = win.Y + WindowTitleMargin;

            if (TitleFont != null)
            {
                var titleBarHeight = TitleFont.GetLineHeight() + (WindowTitleMargin * 2);
                canvas.DrawFilledRectangle(Colors.WindowTitlebar, win.X, win.Y, win.Width, titleBarHeight);

                canvas.DrawString(win.Title, TitleFont, Colors.Text, titleX, titleY);
            }

            win.DrawWidgets(canvas);
        }

        /// <summary>
        /// Updates all windows.
        /// </summary>
        public static void Update()
        {
            foreach (var window in Windows)
                window.UpdateWidgets();
        }

        /// <summary>
        /// Draws all windows onto the canvas
        /// </summary>
        /// <param name="canvas">The canvas to draw the windows onto.</param>
        public static void Draw(Canvas canvas)
        {
            foreach (var window in Windows)
                DrawWindow(canvas, window);
        }
    }
}
