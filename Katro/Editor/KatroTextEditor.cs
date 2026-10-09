using Katro.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Editor
{
    /// <summary>
    /// The text editor.
    /// </summary>
    public static class KatroTextEditor
    {
        /// <summary>
        /// The current document.
        /// </summary>
        public static Document CurrentDocument { get; private set; } = Document.Empty;

        private static bool s_isRunning = false;

        private static void EnsureValidPath()
        {
            if (string.IsNullOrEmpty(CurrentDocument.Path))
            {
                var isValidInput = false;

                while (!isValidInput)
                {
                    Console.Write("File path: ");
                    var input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Logger.Error("Invalid input");
                        continue;
                    }

                    CurrentDocument.Path = input;
                    isValidInput = true;
                }
            }
        }

        private static void ChangeLine()
        {
            var lines = CurrentDocument.Lines.Count;
            Console.Write($"Line number (0 for first line, {lines - 1} for last line): ");

            var lineNumInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lineNumInput))
            {
                Logger.Error("Invalid input");
                return;
            }

            if (!int.TryParse(lineNumInput, out var line))
            {
                Logger.Error("Invalid number");
                return;
            }

            if (line < 0 || line > lines)
            {
                Logger.Error("Index out of range!");
                return;
            }

            var lineContent = CurrentDocument.Lines[line];
            Console.WriteLine($"Current content: {lineContent}");

            Console.Write("New content: ");
            var newContent = Console.ReadLine();

            if (newContent == null)
            {
                Logger.Error("Invalid input");
                return;
            }

            CurrentDocument.Dirty = true;
            CurrentDocument.ChangeLine(line, newContent);
        }

        private static void ExitCmd()
        {
            if (CurrentDocument.Dirty)
            {
                Console.WriteLine("You have unsaved changes!");
                Console.Write("Would you like to save them? [Y/n] ");

                var key = Console.ReadKey();
                var save = key.Key == ConsoleKey.Y;
                Console.WriteLine();

                if (save)
                {
                    EnsureValidPath();
                    CurrentDocument.Save();
                }
            }

            Stop();
        }

        private static void HandleCommand(string cmd)
        {
            switch (cmd)
            {
                case "exit":
                    ExitCmd();
                    break;

                case "save":
                    EnsureValidPath();

                    CurrentDocument.Save();
                    CurrentDocument.Dirty = false;

                    Console.WriteLine("Saved");
                    break;

                case "change_line":
                    ChangeLine();
                    break;

                case "help":
                    Console.WriteLine("/exit        - Exits the text editor");
                    Console.WriteLine("/save        - Saves the current file to disk");
                    Console.WriteLine("/change_line - Changes a line");
                    Console.WriteLine("/refresh     - Refreshes all the changes");
                    break;

                case "refresh":
                    Refresh();
                    break;

                default:
                    Console.WriteLine("Bad command, enter /help to view all available commands");
                    break;
            }
        }

        private static void Refresh()
        {
            Console.Clear();

            for (int l = 0; l < CurrentDocument.Lines.Count; l++)
            {
                var line = CurrentDocument.Lines[l];
                Console.WriteLine($"{l + 1}. {line}");
            }

            CurrentDocument.Line = CurrentDocument.Lines.Count;
        }

        /// <summary>
        /// Runs the editor loop.
        /// </summary>
        public static void Run()
        {
            Console.Clear();

            Console.WriteLine("Katro Text Editor");
            Console.WriteLine("Enter \"/exit\" to quit");
            Console.WriteLine("If you don't want to accidentally run a command but also want to put a slash, start the line with \"!/\"");

            Console.WriteLine();

            for (int l = 0; l < CurrentDocument.Lines.Count; l++)
            {
                var line = CurrentDocument.Lines[l];
                Console.WriteLine($"{l + 1}. {line}");
            }

            CurrentDocument.Line = CurrentDocument.Lines.Count;

            while (s_isRunning)
            {
                Console.Write($"{CurrentDocument.Line + 1}. ");
                var input = Console.ReadLine();

                if (input == null)
                    continue;

                if (input.StartsWith("!/"))
                {
                    CurrentDocument.Dirty = true;
                    CurrentDocument.Lines.Add(input.Substring(1));
                }
                else if (input.StartsWith('/'))
                {
                    HandleCommand(input.Substring(1));
                }
                else
                {
                    CurrentDocument.Dirty = true;
                    CurrentDocument.Lines.Add(input);
                }

                CurrentDocument.Line++;
            }
        }

        /// <summary>
        /// Loads a file.
        /// </summary>
        /// <param name="path">The path of the file to load.</param>
        public static void Load(string path)
        {
            CurrentDocument = new Document(path);
            CurrentDocument.Load();
        }

        /// <summary>
        /// Stops the editor.
        /// </summary>
        public static void Stop()
            => s_isRunning = false;

        /// <summary>
        /// Starts the editor.
        /// </summary>
        public static void Start()
            => s_isRunning = true;
    }
}
