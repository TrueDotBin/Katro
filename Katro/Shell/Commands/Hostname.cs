using Katro.Configuration;
using Katro.Constants.Shell;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    /// <summary>
    /// Gets or sets the current hostname.
    /// </summary>
    public sealed class Hostname : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "hostname";

        /// <inheritdoc/>
        public override string Description { get; } = "Gets or sets the current hostname.";

        /// <inheritdoc/>
        public override string Usage { get; } = "hostname [value]";

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var value = ctx.GetArgument(0);
            
            if (string.IsNullOrEmpty(value))
            {
                var hostname = SystemConfig.Get("hostname", KatroShell.DefaultHostname);
                Console.WriteLine(hostname);
            }
            else
            {
                SystemConfig.Set("hostname", value);
                Console.WriteLine($"hostname = {value}");
            }

            return CommandReturnCode.Success;
        }
    }
}
