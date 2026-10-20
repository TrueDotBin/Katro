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
            const string TestFileContent = "Test file :D";
            const string TestFilePath = "/katro/Test.txt";
            const string TestDirPath = "/katro/Test";
            const string TestFileInDirPath = "/katro/Test/Test2.txt";

            var ctx = new TestContext();

            TestLogger.Header("File System Tests");

            var writeTest = () => File.WriteAllText(TestFilePath, TestFileContent);

            var readTest = () =>
            {
                var content = File.ReadAllText(TestFilePath);
                TestContext.AssertEqual(content, TestFileContent, "Content mismatch");
            };

            var deleteTest = () => File.Delete(TestFilePath);

            // so it doesnt make it a Func<DirectoryInfo>
            var createDirTest = () =>
            {
                Directory.CreateDirectory(TestDirPath);
            };

            var createFileInDirTest = () => File.WriteAllText(TestFileInDirPath, TestFileContent);

            var readFileInDirTest = () =>
            {
                var content = File.ReadAllText(TestFileInDirPath);
                TestContext.AssertEqual(content, TestFileContent, "Content mismatch");
            };

            var deleteFileInDirTest = () => File.Delete(TestFileInDirPath);
            var deleteDirTest = () => Directory.Delete(TestDirPath);

            ctx.Run("Write to file", writeTest);
            ctx.Run("Read from file", readTest);
            ctx.Run("Delete file", deleteTest);
            ctx.Run("Create directory", createDirTest);
            ctx.Run("Write to file in directory", createFileInDirTest);
            ctx.Run("Read from file in directory", readFileInDirTest);
            ctx.Run("Delete file in directory", deleteFileInDirTest);
            ctx.Run("Delete directory", deleteDirTest);

            ctx.PrintOverview();
        }
    }
}
