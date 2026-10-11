using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.GUI.Widgets
{
    /// <summary>
    /// A widget that displays text.
    /// </summary>
    public class Label : Widget
    {
        /// <summary>
        /// The text of this label.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The font to use for this label.
        /// </summary>
        public Font? Font { get; set; } = FontManager.InterRegular;

        /// <summary>
        /// The size of the font in pixels.
        /// </summary>
        /// <remarks>
        /// Only supported on TrueType fonts.
        /// </remarks>
        public int FontSize { get; set; } = 16;

        /// <summary>
        /// The text color.
        /// </summary>
        public Color Foreground { get; set; } = Colors.Text;

        public override void Draw(Canvas canvas)
        {
            if (Font is PCScreenFont psf)
                canvas.DrawString(Text, psf, Foreground, X, Y);
            else if (Font is TrueTypeFont ttf)
                canvas.DrawString(Text, ttf, FontSize, Foreground, X, Y);
        }
    }
}
