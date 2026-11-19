using Katro.Configuration;
using Katro.Constants.FileSystem;
using Katro.Shell.Commands;
using Katro.Shell.Commands.Core;
using Katro.Shell.Commands.System;
using Katro.Shell.Commands.FileSystem;
using TrueDotBin.CosmosUtils.Logging;
using Katro.Shell.Commands.Audio;

namespace Katro.Shell
{
    /// <summary>
    /// The built-in shell for Katro.
    /// </summary>
    public sealed class KatroShell
    {
        /// <summary>
        /// The default hostname.
        /// </summary>
        public const string DefaultHostname = "katro";

        private static Logger s_logger = new("Katro Shell");

        /// <summary>
        /// The current working directory.
        /// </summary>
        public string CurrentDirectory { get; set; } = MountPoints.RootPartition;
            
        /// <summary>
        /// The list of registered commands.
        /// </summary>
        public List<Command> Commands { get; private set; } = [];

        private void WritePrompt()
        {
            var startColor = Console.ForegroundColor;
            var hostname = SystemConfig.Get("hostname", DefaultHostname);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(hostname);
            Console.ForegroundColor = startColor;

            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(CurrentDirectory);
            Console.ForegroundColor = startColor;

            Console.Write("> ");
        }

        /// <summary>
        /// Initializes the shell by registering commands.
        /// </summary>
        public void Initialize()
        {
            Commands.Add(new Help());
            Commands.Add(new Echo());
            Commands.Add(new Hostname());
            Commands.Add(new Shutdown());
            Commands.Add(new Reboot());
            Commands.Add(new FsStat());
            Commands.Add(new Ls());
            Commands.Add(new Clear());
            Commands.Add(new Cd());
            Commands.Add(new Mkdir());
            Commands.Add(new Rm());
            Commands.Add(new Touch());
            Commands.Add(new Cat());
            Commands.Add(new Pwd());
            Commands.Add(new Copy());
            Commands.Add(new Mv());
            Commands.Add(new Commands.System.Services());
            Commands.Add(new Gc());
            Commands.Add(new SysInfo());
            Commands.Add(new Beep());
        }

        /// <summary>
        /// Runs the input loop once.
        /// </summary>
        public void Run()
        {
            WritePrompt();
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return;

            var (name, context) = ShellParser.Parse(input);

            if (context is null)
            {
                s_logger.Log(LogLevel.Error, "Failed to create command context");
                return;
            }

            var cmd = Commands.FirstOrDefault(c => c.Name == name);

            if (cmd is null)
            {
                s_logger.Log(LogLevel.Error, $"Command {name} not found");
                return;
            }

            context.Shell = this;
            var code = cmd.Execute(context);

            if (code != 0)
                s_logger.Log(LogLevel.Error, $"Command {name} exited with non-success exit code: {code}");
        }
    }
}
