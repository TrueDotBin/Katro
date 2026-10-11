using Cosmos.Kernel.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.GUI.Widgets
{
    /// <summary>
    /// A widget, such as a label or a button.
    /// </summary>
    public abstract class Widget
    {
        /// <summary>
        /// The X position of this widget.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y position of this widget.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Updates this widget, use this, for example, to check if the mouse is hovering over something.
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Displays this widget in the canvas.
        /// </summary>
        /// <param name="canvas">The canvas to draw the widget on.</param>
        public abstract void Draw(Canvas canvas);
    }
}
