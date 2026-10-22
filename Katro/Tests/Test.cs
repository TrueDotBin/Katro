using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Tests
{
    /// <summary>
    /// A test that is used to test the functionality of a specific thing.
    /// </summary>
    public class Test(string name, Action run)
    {
        /// <summary>
        /// The name of the test.
        /// </summary>
        public string Name { get; init; } = name;

        /// <summary>
        /// The action that will be ran.
        /// </summary>
        public Action OnRun { get; init; } = run;

        /// <summary>
        /// Runs the test.
        /// </summary>
        /// <param name="ctx">The test context.</param>
        public void Run(TestContext ctx)
            => ctx.Run(Name, OnRun);
    }
}
