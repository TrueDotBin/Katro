using TrueDotBin.CosmosUtils.Logging;
using Katro.Services.Abstractions;
using Katro.Configuration;
using Katro.FileSystem;

namespace Katro.Services.Core
{
    /// <summary>
    /// Loads and saves the configuration.
    /// </summary>
    public class ConfigService : KernelService
    {
        /// <inheritdoc/>
        public override string Name { get; } = "Configuration";

        /// <inheritdoc/>
        public override string Id { get; } = "cfg";

        /// <inheritdoc/>
        public override string[] DependsOn { get; } = ["fs"];

        /// <inheritdoc/>
        public override bool Start()
        {
            Logger.Log(LogLevel.Info, $"Loading configuration from {Paths.ConfigFilePath}");

            try
            {
                SystemConfig.Load();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Failed to load configuration: {ex.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            Logger.Log(LogLevel.Info, $"Saving configuration to {Paths.ConfigFilePath}");

            try
            {
                SystemConfig.Save();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Failed to save configuration: {ex.Message}");
            }
        }
    }
}
