using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell
{
    /// <summary>
    /// Parses command-line arguments.
    /// </summary>
    public static class ShellParser
    {
        /// <summary>
        /// The flag/option prefix.
        /// </summary>
        public const string FlagPrefix = "--";

        /// <summary>
        /// The list of valid characters that will be treated as a quoted string.
        /// </summary>
        public static readonly char[] QuoteCharacters = ['\'', '"'];

        /// <summary>
        /// Splits the input string by quotes.
        /// </summary>
        /// <param name="value">The string that should be split.</param>
        public static List<string> SplitQuotes(string value)
        {
            var insideQuote = false;
            var current = new StringBuilder();
            var output = new List<string>();

            for (int i = 0; i < value.Length; i++)
            {
                var ch = value[i];

                if (QuoteCharacters.Contains(ch))
                {
                    insideQuote = !insideQuote;
                }
                else if (char.IsWhiteSpace(ch))
                {
                    if (!insideQuote)
                    {
                        output.Add(current.ToString());
                        current.Clear();
                    }
                    else
                        current.Append(ch);
                }
                else
                    current.Append(ch);
            }

            output.Add(current.ToString());
            return output;
        }

        /// <summary>
        /// Parses the provided <paramref name="input"/> into a command context.
        /// </summary>
        /// <param name="input">The user input that should be parsed into a command context.</param>
        /// <returns>The command's name and context.</returns>
        public static (string CommandName, CommandContext? Context) Parse(string input)
        {
            var parts = SplitQuotes(input);

            if (parts.Count == 0)
                return (string.Empty, null);

            var context = new CommandContext(input);
            var name = parts.First();

            for (int i = 1; i < parts.Count; i++)
            {
                var part = parts[i];

                if (part.StartsWith(FlagPrefix))
                {
                    if (part.Contains('='))
                    {
                        var lastEquals = part.LastIndexOf('=');
                        var optName = part.Substring(FlagPrefix.Length, lastEquals);
                        var optValue = part.Substring(lastEquals + 1);

                        context.Options[optName] = optValue;
                    }
                    else
                    {
                        var flagName = part.Substring(FlagPrefix.Length);
                        context.Flags.Add(flagName);
                    }
                }
                else
                {
                    context.Args.Add(part);
                }
            }

            return (name, context);
        }
    }
}
