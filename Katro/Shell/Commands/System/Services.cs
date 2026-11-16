using Katro.Configuration;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Displays all currently running services.
    /// </summary>
    public sealed class Services : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "services";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays all currently running services.";

        /// <inheritdoc/>
        public override string Usage { get; } = "services";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var services = ServiceHost.GetServices();

            foreach (var svc in services)
            {
                Terminal.WriteColored($"[ {svc.Id,-8} ] ", ConsoleColor.Cyan);
                Console.Write($"{svc.Name,-20}");

                if (svc.DependsOn.Length > 0)
                {
                    var deps = string.Join(", ", svc.DependsOn);
                    Terminal.WriteLineColored($"Depends on: {deps}", ConsoleColor.DarkGray);
                }
                else
                {
                    Console.WriteLine("No dependencies");
                }
            }

            return CommandReturnCode.Success;
        }
    }
}
