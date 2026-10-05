using Cosmos.Kernel.System;

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

            Console.Clear();

            Console.WriteLine("Katro System Error");
            Console.WriteLine();
            Console.WriteLine("The system has detected a failure.");
            Console.WriteLine("It has now stopped to prevent data loss.");
            Console.WriteLine(message);
            Console.WriteLine("Press ENTER to reboot.");
            Console.WriteLine();
            Console.WriteLine("Additional details:");
            Console.WriteLine($"Error code: {errorCode}");

            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.ResetColor();
                    Console.Clear();
                    Console.WriteLine("Rebooting");

                    Power.Reboot();
                }
            }
        }
    }
}
