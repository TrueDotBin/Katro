using Cosmos.Kernel.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.GUI
{
    /// <summary>
    /// Holds all the fonts.
    /// </summary>
    public static class FontManager
    {
        /// <summary>
        /// The Inter Regular font.
        /// </summary>
        public static TrueTypeFont? InterRegular { get; set; }

        /// <summary>
        /// The Inter Bold font.
        /// </summary>
        public static TrueTypeFont? InterBold { get; set; }
    }
}
