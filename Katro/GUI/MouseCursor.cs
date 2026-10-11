using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Input;
using Katro.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.GUI
{
    /// <summary>
    /// A mouse cursor.
    /// </summary>
    public static class MouseCursor
    {
        /// <summary>
        /// The cursor image.
        /// </summary>
        public static Image? CursorImage { get; set; }

        private static byte[] s_defaultCursorBytes = [];

        /// <summary>
        /// Sets the cursor image to the default one.
        /// </summary>
        public static void SetDefaultImage()
        {
            if (s_defaultCursorBytes.Length == 0)
                s_defaultCursorBytes = EmbeddedResourceManager.GetEmbeddedResource("Katro.Resources.Images.Cursors.cursor.png");

            CursorImage = new Png(s_defaultCursorBytes);
        }

        /// <summary>
        /// Draws the mouse cursor in a canvas.
        /// </summary>
        /// <param name="canvas">The canvas to draw the cursor on.</param>
        public static void Draw(Canvas canvas)
        {
            if (CursorImage == null)
                return;

            var x = MouseManager.X;
            var y = MouseManager.Y;

            canvas.DrawImage(CursorImage, x, y);
        }
    }
}
