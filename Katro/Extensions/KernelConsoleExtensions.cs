using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;
using Katro.Utils;

namespace Katro.Extensions
{
    public static class KernelConsoleExtensions
    {
        /// <summary>
        /// Sets a font from an embedded resource.
        /// </summary>
        /// <param name="console">The <see cref="KernelConsole"/> object.</param>
        /// <param name="resourceName">The embedded resource's name.</param>
        /// <param name="isTrueType">Whether to use TrueType font loading instead of PC Screen Font loading.</param>
        public static void SetFontFromResource(this KernelConsole console, string resourceName, bool isTrueType = false)
        {
            var resourceArray = EmbeddedResourceManager.GetEmbeddedResource(resourceName);

            if (isTrueType)
                console.Font = new TrueTypeFont(resourceArray);
            else
                console.Font = PCScreenFont.LoadFont(resourceArray);
        }
    }
}
