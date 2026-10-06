using Cosmos.Kernel.System;
using System.Text;

namespace Katro.Terminal
{
    /// <summary>
    /// The error screen, which tells the user what went wrong.
    /// This is different from using <see cref="Logging.Logger.Error(string)"/>.
    /// </summary>
    public static class ErrorScreen
    {
        /// <summary>
        /// Shows the error screen.
        /// </summary>
        /// <param name="message">A short description of what went wrong.</param>
        /// <param name="errorCode">A short string that must be in SCREAMING_SNAKE_CASE identifying the error (e.g., "INVALID_INDEX")</param>
        public static void Show(string message, string errorCode)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            Console.Clear();

            Console.WriteLine("SYSTEM ERROR");

            Console.WriteLine("Katro has encountered an error that the system couldn't handle.");
            Console.WriteLine("The system has stopped to prevent data loss.");
            Console.WriteLine($"Error message: {message}");
            Console.WriteLine();
            Console.WriteLine("=== ADDITIONAL INFORMATION ===");
            Console.WriteLine($"Error code: {errorCode}");

            Console.CursorTop = Console.WindowHeight - 1;
            Console.Write("Press ENTER to reboot");

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    Power.Reboot();
            }
        }
    }
}