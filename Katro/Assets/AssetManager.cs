using Cosmos.Kernel.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Katro.Assets
{
    /// <summary>
    /// Manages Katro's assets, such as fonts, images, etc.
    /// </summary>
    public static class AssetManager
    {
        /// <summary>
        /// The font used for the console.
        /// </summary>
        public static PCScreenFont ConsoleFont { get; private set; } = PCScreenFont.DefaultFont;

        /// <summary>
        /// THe raw bytes of the ktsh.lua file.
        /// </summary>
        public static byte[] KatroShell { get; private set; } = [];

        private static readonly Assembly s_assembly = typeof(Kernel).Assembly;

        private static byte[] LoadResourceFromAssembly(string path)
        {
            using var stream = s_assembly.GetManifestResourceStream(path);
            using var ms = new MemoryStream();

            stream?.CopyTo(ms);

            return ms.ToArray();
        }

        /// <summary>
        /// Loads all the assets.
        /// </summary>
        public static void Load()
        {
            var consoleFontBytes = LoadResourceFromAssembly("Katro.Resources.Fonts.PSF.Spleen.psfu");
            ConsoleFont = PCScreenFont.LoadFont(consoleFontBytes);

            KatroShell = LoadResourceFromAssembly("Katro.Resources.Scripts.ktsh.lua");
        }
    }
}
