using Katro.Configuration;
using Katro.Constants.FileSystem;
using Katro.Shell.Commands;
using Katro.Shell.Commands.Core;
using Katro.Shell.Commands.System;
using Katro.Shell.Commands.FileSystem;
using TrueDotBin.CosmosUtils.Logging;
using Katro.Shell.Commands.Audio;
using Katro.Output;
using Katro.Output.Sinks;
using Katro.FileSystem;
using Katro.Constants.Shell;
using Katro.Users;
using Katro.Shell.Commands.Network;

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
            var hostname = SystemConfig.Get("hostname", DefaultHostname);

            Terminal.Write(hostname, ConsoleColor.Green);
            Terminal.Write("@");
            Terminal.Write(CurrentDirectory, ConsoleColor.Yellow);
            Terminal.Write("> ");
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
            Commands.Add(new Play());
            Commands.Add(new Whoami());
            Commands.Add(new Su());
            Commands.Add(new Passwd());
            Commands.Add(new AddUser());
            Commands.Add(new Sudo());
            Commands.Add(new RmUser());
            Commands.Add(new Commands.System.KTLang());
            Commands.Add(new NsResolve());
            Commands.Add(new Ping());
            Commands.Add(new IpConfig());
            Commands.Add(new Curl());
            Commands.Add(new Download());
        }

        /// <summary>
        /// Runs a command based on the input.
        /// </summary>
        public void ProcessInput(string input)
        {
            var (name, context, file, append) = ShellParser.Parse(input);

            if (context is null)
            {
                s_logger.Log(LogLevel.Error, "Failed to create command context");
                return;
            }

            var cmd = Commands.FirstOrDefault(c => c.Name == name);

            if (cmd is null)
            {
                s_logger.Log(LogLevel.Error, $"Command \"{name}\" not found");
                return;
            }

            context.Shell = this;

            var isSuperuser = UserManager.IsType(Enums.UserType.Superuser) || UserManager.Elevated;
            if (cmd.RequiresSuperuser && !isSuperuser)
            {
                s_logger.Log(LogLevel.Error, "Permission denied");
                return;
            }

            if (!string.IsNullOrEmpty(file))
            {
                var fullPath = PathHelper.Resolve(CurrentDirectory, file);
                Terminal.CurrentOutput = new FileSink(fullPath, append);
            }

            try
            {
                var code = cmd.Execute(context);

                if (code != CommandReturnCode.Success)
                {
                    switch (code)
                    {
                        // this is used in the help command
                        case CommandReturnCode.CommandNotFound:
                            s_logger.Log(LogLevel.Error, $"Command not found");
                            break;

                        case CommandReturnCode.GeneralFailure:
                            s_logger.Log(LogLevel.Error, $"Command \"{name}\" failed");
                            break;

                        case CommandReturnCode.MissingArgument:
                            s_logger.Log(LogLevel.Error, $"Missing argument for \"{name}\"");
                            break;

                        case CommandReturnCode.NoEntry:
                            s_logger.Log(LogLevel.Error, "No such file or directory");
                            break;

                        case CommandReturnCode.InvalidPermissions:
                            s_logger.Log(LogLevel.Error, "Permission denied");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Terminal.CSharpException(ex);
            }
            finally
            {
                Terminal.CurrentOutput.Dispose();
                Terminal.CurrentOutput = new ConsoleSink();
            }
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

            ProcessInput(input);
        }
    }
}
