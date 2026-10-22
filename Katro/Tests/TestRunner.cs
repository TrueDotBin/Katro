using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Tests
{
    /// <summary>
    /// All the kernel tests.
    /// </summary>
    public static class TestRunner
    {
        /// <summary>
        /// Runs a test.
        /// </summary>
        /// <param name="testName">The name of the test.</param>
        /// <param name="test"></param>
        /// <returns>Whether the test has passed.</returns>
        public static bool RunTest(string testName, Action? test)
        {
            try
            {
                test?.Invoke();
                TestLogger.Pass(testName);

                return true;
            }
            catch (Exception ex)
            {
                TestLogger.Fail(testName, ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Starts all the file system tests.
        /// </summary>
        public static void FileSystemTests()
        {
            var ctx = new TestContext("File System Tests");

            var tests = new List<Test>()
            {
                new("Write to File", () => File.WriteAllText(TestConstants.TestFilePath, TestConstants.TestFileContents)),
                new("Read from File", () =>
                {
                    var actualContents = File.ReadAllText(TestConstants.TestFilePath);
                    TestContext.AssertEqual(actualContents, TestConstants.TestFileContents, "Content mismatch");
                }),
                new("Delete File", () => File.Delete(TestConstants.TestFilePath)),
                new("Create Directory", () => Directory.CreateDirectory(TestConstants.TestDirPath)),
                new("Delete Directory", () => Directory.Delete(TestConstants.TestDirPath))
            };

            foreach (var test in tests)
                test.Run(ctx);

            ctx.PrintOverview();
        }
    }
}
