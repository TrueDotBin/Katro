using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
using Katro.Security;
using Katro.Users;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Katro.Shell.Commands.FileSystem
{
    /// <summary>
    /// Reads a file, or multiple files.
    /// </summary>
    public class Cat : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "cat";

        /// <inheritdoc/>
        public override string Description { get; } = "Reads a file, or multiple files.";

        /// <inheritdoc/>
        public override string Usage { get; } = "cat <paths>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.FileSystem;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            foreach (var argument in ctx.Args)
            {
                var path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, argument);

                if (!File.Exists(path))
                {
                    Terminal.WriteLine($"No such file: {path}");
                    continue;
                }

                if (!PermissionHelper.CanAccessPath(path))
                {
                    Terminal.WriteLine($"Cannot read sensitive file {path}");
                    continue;
                }

                var contents = File.ReadAllText(path);
                Terminal.WriteLine(contents);
            }

            return CommandReturnCode.Success;
        }
    }
}
