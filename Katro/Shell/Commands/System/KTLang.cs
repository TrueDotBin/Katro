using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Output;
using Katro.Scripting;
using Katro.Security;
using Katro.Users;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Executes a KTLang script.
    /// </summary>
    public sealed class KTLang : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "ktlang";

        /// <inheritdoc/>
        public override string Description { get; } = "Executes a KTLang script.";

        /// <inheritdoc/>
        public override string Usage { get; } = "ktlang [path]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        private void StartRepl()
        {
            Terminal.WriteLine("KTLang Interpreter");
            Terminal.WriteLine("Type 'exit' to quit.");

            while (true)
            {
                Terminal.Write("ktlang> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Contains("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                KTLangRunner.RunString(input);
            }
        }

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            if (!KTLangRunner.Initialized)
                KTLangRunner.Initialize(new KatroHost());
            else
                KTLangRunner.Reinitialize();

            var path = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(path))
            {
                StartRepl();
                return CommandReturnCode.Success;
            }

            path = PathHelper.Resolve(ctx.Shell.CurrentDirectory, path);

            if (!File.Exists(path))
                return CommandReturnCode.NoEntry;

            if (!PermissionHelper.CanAccessPath(path))
                return CommandReturnCode.InvalidPermissions;

            var contents = File.ReadAllText(path);
            KTLangRunner.RunString(contents);

            return CommandReturnCode.Success;
        }
    }
}
