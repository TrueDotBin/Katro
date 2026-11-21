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
        /// The character that indicates that the output should be redirected to a file.
        /// </summary>
        public const string RedirectChar = ">";

        /// <summary>
        /// The character that indicates that the output should be appended to a file.
        /// </summary>
        public const string AppendChar = ">>";

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
        public static (string CommandName, CommandContext? Context, string? RedirectFile, bool Append) Parse(string input)
        {
            var parts = SplitQuotes(input);

            if (parts.Count == 0)
                return (string.Empty, null, null, false);

            var context = new CommandContext(input);
            var name = parts.First();
            var append = false;
            string? redirectFile = null;

            for (int i = 1; i < parts.Count; i++)
            {
                var part = parts[i];

                if (part == RedirectChar || part == AppendChar)
                {
                    if (i + 1 < parts.Count)
                    {
                        redirectFile = parts[i + 1];
                        append = part == AppendChar;
                        break;
                    }
                }

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

            return (name, context, redirectFile, append);
        }
    }
}
