using Katro.Enums;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class HelpCommand : Command
    {
        public override string Name { get; } = "help";
        public override string Description { get; } = "Displays available commands.";
        public override string Usage { get; } = "help [command]";
        public override string[] Aliases { get; } = ["commands", "cmdls"];
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("command", 0, "If specified, prints info about that command", false)
        ];

        private void PrintInfoAboutCommand(Command command)
        {
            Console.WriteLine($"{command.Name} - {command.Description}");
            Console.WriteLine($"Usage: {command.Usage}");

            if (command.Aliases.Length > 0)
            {
                Console.WriteLine();

                Console.WriteLine("Aliases:");

                foreach (var alias in command.Aliases)
                {
                    Console.WriteLine($"\t{alias}");
                }
            }

            if (command.Args.Length > 0)
            {
                Console.WriteLine();

                Console.WriteLine("Arguments:");

                foreach (var arg in command.Args)
                {
                    if (arg is OptionArgument opt)
                    {
                        var argString = $"{ArgumentParser.LongOptPrefix}{opt.Key}";

                        if (!string.IsNullOrEmpty(opt.ShortKey))
                            argString += $"/{ArgumentParser.ShortOptPrefix}{opt.ShortKey}";

                        Console.Write($"\t{argString} ");

                        if (!opt.Required)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("(optional) ");
                            Console.ResetColor();
                        }

                        Console.WriteLine($"- {opt.Description}");
                    }
                    else
                    {
                        Console.Write($"\t{arg.Name} ");

                        if (!arg.Required)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("(optional) ");
                            Console.ResetColor();
                        }

                        Console.WriteLine($"- {arg.Description}");
                    }
                }
            }
        }

        public override int Run()
        {
            if (TryGetPositional(0, out var commandName))
            {
                var name = commandName.Value?.ToString();

                if (string.IsNullOrEmpty(name))
                    return (int)CommandReturnCode.BadArgument;

                var command = CommandManager.Get(name);

                if (command == null)
                    return (int)CommandReturnCode.CommandNotFound;

                PrintInfoAboutCommand(command);
            }
            else
            {
                foreach (var command in CommandManager.Commands)
                {
                    Console.Write($"{command.Name} - ");

                    if (!string.IsNullOrEmpty(command.Description))
                    {
                        Console.WriteLine(command.Description);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No description provided");
                    }

                    Console.ResetColor();
                }
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
