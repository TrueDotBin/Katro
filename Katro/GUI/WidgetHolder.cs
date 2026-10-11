using Cosmos.Kernel.System.Graphics;
using Katro.GUI.Widgets;
using Katro.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.GUI
{
    /// <summary>
    /// Holds widgets.
    /// </summary>
    public class WidgetHolder
    {
        /// <summary>
        /// The X position of this widget holder.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y position of this widget holder.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of this widget holder.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of this widget holder.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The list of widgets.
        /// </summary>
        public List<Widget> Widgets { get; set; } = [];

        /// <summary>
        /// Updates all widgets.
        /// </summary>
        public void UpdateWidgets()
        {
            foreach (var widget in Widgets)
                widget.Update();
        }

        /// <summary>
        /// Draws all widgets onto the canvas.
        /// </summary>
        /// <param name="canvas">The canvas to draw the widgets on.</param>
        public void DrawWidgets(Canvas canvas)
        {
            const int margin = 10;

            foreach (var widget in Widgets)
            {
                var oldX = widget.X;
                var oldY = widget.Y;

                var height = WindowManager.TitleFont?.Height ?? 0;
                var yMargin = height + (WindowManager.WindowTitleMargin * 2);

                widget.X = oldX + X;
                widget.Y = oldY + Y + yMargin + margin;

                widget.Draw(canvas);

                widget.X = oldX;
                widget.Y = oldY;
            }
        }
    }
}
