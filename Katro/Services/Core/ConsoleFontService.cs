using Katro.Services.Abstractions;
using TrueDotBin.CosmosUtils.Resources;
using TrueDotBin.CosmosUtils.Logging;
using Cosmos.Kernel.System.Graphics.Fonts;
using Cosmos.Kernel.System.Graphics;

namespace Katro.Services.Core
{
    /// <summary>
    /// Sets the console's font.
    /// </summary>
    public class ConsoleFontService : KernelService
    {
        /// <inheritdoc/>
        public override string Name { get; } = "Console Font";

        /// <inheritdoc/>
        public override string Id { get; } = "fnt";

        /// <inheritdoc/>
        public override bool Start()
        {
            try
            {
                var assembly = typeof(Kernel).Assembly;

                if (!EmbeddedResourceLoader.TryLoadResource("Katro.Resources.Fonts.Psf.Spleen.psfu", assembly, out var font))
                {
                    Logger.Log(LogLevel.Error, "Embedded font missing! Using built-in font instead");
                    return true; // technically did its job
                }

                var psf = PCScreenFont.LoadFont(font);
                KernelConsole.Default?.Font = psf;

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Failed to load font: {ex.Message}");

                if (!string.IsNullOrEmpty(ex.StackTrace))
                    Logger.Log(LogLevel.Error, $"Stack Trace:\n{ex.StackTrace}");

                return false;
            }
        }
    }
}
