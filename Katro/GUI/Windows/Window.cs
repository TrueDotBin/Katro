using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.GUI.Windows
{
    /// <summary>
    /// A window.
    /// </summary>
    public class Window : WidgetHolder
    {
        /// <summary>
        /// The title of this window.
        /// </summary>
        public string Title { get; set; } = "Window";

        /// <summary>
        /// The bounds of this window, used for dragging the window.
        /// </summary>
        public Rectangle DragBounds => new(X, Y, Width, WindowManager.TitlebarHeight);
    }
}
