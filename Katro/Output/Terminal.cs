using Katro.Output.Sinks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Output
{
    /// <summary>
    /// Utilities for printing fancy messages.
    /// </summary>
    public static class Terminal
    {
        /// <summary>
        /// The current output sink that this utility writes to.
        /// </summary>
        public static OutputSink CurrentOutput { get; set; } = new ConsoleSink();

        private static (ConsoleColor PrevForeground, ConsoleColor PrevBackground) SetColor(ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var oldFg = CurrentOutput.ForegroundColor;
            var oldBg = CurrentOutput.BackgroundColor;

            CurrentOutput.ForegroundColor = fg ?? oldFg;
            CurrentOutput.BackgroundColor = bg ?? oldBg;

            return (oldFg, oldBg);
        }

        /// <summary>
        /// Displays a formatted heading.
        /// </summary>
        /// <param name="label">What this heading grpups.</param>
        public static void Heading(object label)
        {
            Write("===== ", ConsoleColor.Gray);
            Write(label);
            WriteLine(" =====", ConsoleColor.Gray);
        }

        /// <summary>
        /// Displays a formatted information line.
        /// </summary>
        /// <param name="label">The description label.</param>
        /// <param name="value">The value to display.</param>
        public static void InfoLine(string label, object value)
        {
            Write($"{label,-30}", ConsoleColor.Yellow);
            WriteLine(value, ConsoleColor.Gray);
        }

        /// <summary>
        /// Primts a value with the specified foreground and background, with a trailing line break.
        /// </summary>
        /// <param name="value">The value that should be written into the console.</param>
        /// <param name="fg">The color of the text.</param>
        /// <param name="bg">The color of the text's background.</param>
        public static void WriteLine(object? value = null, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var (oldFg, oldBg) = SetColor(fg, bg);

            CurrentOutput.WriteLine(value);

            CurrentOutput.ForegroundColor = oldFg;
            CurrentOutput.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Primts a value with the specified foreground and background.
        /// </summary>
        /// <param name="value">The value that should be written into the console.</param>
        /// <param name="fg">The color of the text.</param>
        /// <param name="bg">The color of the text's background.</param>
        public static void Write(object value, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            var (oldFg, oldBg) = SetColor(fg, bg);

            CurrentOutput.Write(value);

            CurrentOutput.ForegroundColor = oldFg;
            CurrentOutput.BackgroundColor = oldBg;
        }

        /// <summary>
        /// Displays a formatted C# exception.
        /// </summary>
        /// <param name="ex">The exception that should be displayed.</param>
        public static void CSharpException(Exception ex)
        {
            WriteLine(ex.Message, ConsoleColor.Red);

            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                CurrentOutput.WriteLine();

                Heading("Stack Trace");
                WriteLine(ex.StackTrace, ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Displays an interactive selection prompt.
        /// </summary>
        /// <param name="prompt">The text to display above the options.</param>
        /// <param name="options">The list of options to select from.</param>
        /// <returns>The index of the selected option.</returns>
        public static int Select(string prompt, params IEnumerable<string> options)
        {
            var optionList = options.ToList();
            if (optionList.Count == 0)
                return -1;

            if (optionList.Count == 1)
                return 0; // first element, there's no point in asking the user for a choice

            CurrentOutput.WriteLine(prompt);

            var currentChoice = 0;
            var startTop = Console.CursorTop;
            var optionsLength = optionList.Count;

            while (true)
            {
                Console.CursorTop = startTop;

                for (int i = 0; i < optionsLength; i++)
                {
                    var optionLabel = optionList[i];

                    if (i == currentChoice)
                        WriteLine($"> {optionLabel}", ConsoleColor.White);
                    else
                        WriteLine($"  {optionLabel}", ConsoleColor.DarkGray);
                }

                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && currentChoice > 0)
                {
                    currentChoice--;
                }
                else if (key == ConsoleKey.DownArrow && currentChoice < optionsLength - 1)
                {
                    currentChoice++;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.CursorTop = startTop + optionsLength;
                    return currentChoice;
                }
            }
        }

        /// <summary>
        /// Clears the current output.
        /// </summary>
        public static void Clear()
            => CurrentOutput.Clear();
    }
}
