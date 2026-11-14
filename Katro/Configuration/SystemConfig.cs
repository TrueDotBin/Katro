using Katro.Constants.FileSystem;
using System.Text;

namespace Katro.Configuration
{
    /// <summary>
    /// Manages the current configuration.
    /// </summary>
    public static class SystemConfig
    {
        /// <summary>
        /// The path of the config file.
        /// </summary>
        public static readonly string ConfigFilePath
            = Path.Combine(MountPoints.RootPartition, "config.cfg");

        private static Dictionary<string, string> _values = [];

        /// <summary>
        /// Loads the current configuration file.
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(ConfigFilePath))
                return;

            var lines = File.ReadAllLines(ConfigFilePath);

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                var shouldSkip = !trimmed.Contains('=') || trimmed.StartsWith(';');

                if (shouldSkip)
                    continue;

                var parts = trimmed.Split('=');
                var key = parts[0];
                var value = parts[1];

                _values[key] = value;
            }
        }

        /// <summary>
        /// Saves the current configuration to a file.
        /// </summary>
        public static void Save()
        {
            var sb = new StringBuilder();

            sb.AppendLine("; Configuration file for Katro");

            foreach (var kv in _values)
            {
                sb.AppendLine($"{kv.Key} = {kv.Value}");
            }

            File.WriteAllText(ConfigFilePath, sb.ToString());
        }
    }
}
