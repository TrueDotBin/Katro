using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang
{
    /// <summary>
    /// Built-in KTLang functions.
    /// </summary>
    public static class Builtins
    {
        /// <summary>
        /// The standard delegate for all built-in functions.
        /// </summary>
        /// <param name="args">The list of arguments that should be provided to the function.</param>
        public delegate object? NativeFunction(List<object?> args, IKatroHost host);

        /// <summary>
        /// The list of names that resolve to built-in functions.
        /// </summary>
        public static readonly Dictionary<string, NativeFunction> Registry = new()
        {
            { "println", PrintLn }
        };

        /// <summary>
        /// Implementation for println(args): Displays a message.
        /// </summary>
        public static object? PrintLn(List<object?> args, IKatroHost host)
        {
            var output = string.Join(" ", args);
            host.PrintLn(output);

            return null;
        }
    }
}
