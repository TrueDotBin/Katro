using Katro.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Tests
{
    /// <summary>
    /// Additional stuff for logging inside the kernel test.
    /// </summary>
    public static class TestLogger
    {
        /// <summary>
        /// Prints a header.
        /// </summary>
        /// <param name="title">The title of the header</param>
        public static void Header(string title)
            => Console.WriteLine($"===== {title} =====");

        /// <summary>
        /// Indicates that a test has passed.
        /// </summary>
        /// <param name="testName">The name of the test.</param>
        public static void Pass(string testName)
        {
            Logger.Ok($"PASS({testName})");
        }

        /// <summary>
        /// Indicates that a test has fail.
        /// </summary>
        /// <param name="testName">The name of the test.</param>
        /// <param name="message">An optional message.</param>
        public static void Fail(string testName, string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Logger.Error($"FAIL({testName})");
                return;
            }

            Logger.Error($"FAIL({testName}): {message}");
        }
    }
}
