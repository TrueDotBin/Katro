using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using Cosmos.Kernel.System.Input;
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
        /// The height of the title bar.
        /// </summary>
        public static int TitlebarHeight { get; private set; }

        /// <summary>
        /// The title font.
        /// </summary>
        public static readonly Font? TitleFont = FontManager.InterBold;

        /// <summary>
        /// The window title margin.
        /// </summary>
        public const int WindowTitleMargin = 5;

        private static Window? s_currentlyDragging;
        private static int s_dragOffsetX;
        private static int s_dragOffsetY;

        private static void DrawWindow(Canvas canvas, Window win)
        {
            canvas.DrawFilledRectangle(Colors.Base, win.X, win.Y, win.Width, win.Height);

            var titleX = win.X + WindowTitleMargin;
            var titleY = win.Y + WindowTitleMargin;

            if (TitleFont != null)
            {
                TitlebarHeight = TitleFont.GetLineHeight() + (WindowTitleMargin * 2);
                canvas.DrawFilledRectangle(Colors.WindowTitlebar, win.X, win.Y, win.Width, TitlebarHeight);

                canvas.DrawString(win.Title, TitleFont, Colors.Text, titleX, titleY);
            }

            win.DrawWidgets(canvas);
        }

        /// <summary>
        /// Updates all windows.
        /// </summary>
        public static void Update()
        {
            if (s_currentlyDragging != null)
            {
                if (!MouseManager.LeftButton)
                    s_currentlyDragging = null;
                
                s_currentlyDragging?.X = MouseManager.X - s_dragOffsetX;
                s_currentlyDragging?.Y = MouseManager.Y - s_dragOffsetY;
            }

            foreach (var window in Windows)
            {
                window.UpdateWidgets();

                if (MouseManager.LeftButton)
                {
                    var mouseX = MouseManager.X;
                    var mouseY = MouseManager.Y;

                    var mouseBounds = new Rectangle(mouseX, mouseY, 1, 1);

                    if (window.DragBounds.IntersectsWith(mouseBounds))
                    {
                        s_currentlyDragging = window;

                        s_dragOffsetX = mouseX - window.X;
                        s_dragOffsetY = mouseY - window.Y;
                    }
                }
            }
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
