using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Tests
{
    /// <summary>
    /// Keeps track of all tests that have failed or passed.
    /// </summary>
    public class TestContext
    {
        /// <summary>
        /// The category of the tests that will be run.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// The amount of tests that have failed.
        /// </summary>
        public int Failed { get; private set; }

        /// <summary>
        /// The amount of tests that have passed.
        /// </summary>
        public int Passed { get; private set; }

        /// <summary>
        /// Initializes a new test context.
        /// </summary>
        /// <param name="category">The category of the tests that will be run.</param>
        public TestContext(string category)
        {
            Category = category;
            TestLogger.Header(category);
        }

        /// <summary>
        /// Runs a test.
        /// </summary>
        /// <param name="name">The name of the test.</param>
        /// <param name="test">The action that gets invoked.</param>
        public void Run(string name, Action test)
        {
            if (TestRunner.RunTest(name, test))
                Passed++;
            else
                Failed++;
        }

        /// <summary>
        /// Prints a summary of all the tests that have passed and failed.
        /// </summary>
        public void PrintOverview()
        {
            var total = Passed + Failed;

            Console.WriteLine("Test Overview:");
            Console.WriteLine($"\t{total} test(s) in total");
            Console.WriteLine($"\t{Passed} passed");
            Console.WriteLine($"\t{Failed} failed");
        }

        /// <summary>
        /// Determines whether <paramref name="actual"/> is equal to <paramref name="expected"/>.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="errorMessage">The message to use if <paramref name="actual"/> and <paramref name="expected"/> are not equal.</param>
        /// <exception cref="Exception">Thrown when <paramref name="actual"/> and <paramref name="expected"/> are not equal.</exception>
        public static void AssertEqual(object actual, object expected, string errorMessage)
        {
            if (!object.Equals(actual, expected))
                throw new Exception($"{errorMessage} (Expected {expected} but got {actual})");
        }
    }
}
