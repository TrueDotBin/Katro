using Katro.Logging;
using Katro.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Parsing
{
    /// <summary>
    /// Parses a <see cref="Command">'s arguments.
    /// </summary>
    public static class ArgumentParser
    {
        private const string LongOptPrefix = "--";
        private const string ShortOptPrefix = "-";

        private static bool IsValidOpt(string value, OptionArgument opt)
            => opt.Key == value || opt.ShortKey == value;

        private static void ParseOption(Command command, string arg, string prefix)
        {
            var substr = arg.Substring(prefix.Length);
            var option = command.Args.FirstOrDefault(a => a is OptionArgument opt && IsValidOpt(substr, opt));

            if (option == null)
                return;

            option.Value = substr;
        }

        /// <summary>
        /// Splits a string, while keeping quotes as one entry instead of multiple.
        /// </summary>
        /// <param name="input">The input string to parse.</param>
        public static List<string> SplitQuotes(string input)
        {
            var current = new StringBuilder();
            var inQuotes = false;
            var output = new List<string>();

            foreach (var c in input)
            {
                if (c == '\'' || c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (!inQuotes)
                    {
                        output.Add(current.ToString());
                        current.Clear();
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
                else
                {
                    current.Append(c);
                }
            }

            if (current.Length > 0)
                output.Add(current.ToString());

            return output;
        }

        /// <summary>
        /// Parses the input string for a command.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <return>The parsed command.</return>
        public static Command? Parse(string input)
        {
            var parts = SplitQuotes(input);

            if (parts.Count == 0)
                return null;

            var name = parts.First();
            var args = parts.Count > 1 ? parts.Skip(1) : [];

            var positionalIndex = 0;

            var command = CommandManager.Get(name);

            if (command == null)
                return null;

            foreach (var arg in args)
            {
                if (arg.StartsWith(LongOptPrefix))
                {
                    ParseOption(command, arg, LongOptPrefix);
                }
                else if (arg.StartsWith(ShortOptPrefix))
                {
                    ParseOption(command, arg, ShortOptPrefix);
                }
                else
                {
                    var positional = command.Args.FirstOrDefault(a => a is PositionalArgument pos && pos.Position == positionalIndex);

                    if (positional == null)
                        continue;

                    positional.Value = arg;
                    positionalIndex++;
                }
            }

            return command;
        }
    }
}
