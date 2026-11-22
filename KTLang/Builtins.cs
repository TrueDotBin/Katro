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
            { "println", PrintLn },
            { "print", Print },
            { "len", Len },
            { "toString", ToStr }
        };

        /// <summary>
        /// Implementation for println(args): Displays a message with a trailing new line.
        /// </summary>
        public static object? PrintLn(List<object?> args, IKatroHost host)
        {
            var output = string.Join(" ", args);
            host.PrintLn(output);

            return null;
        }

        /// <summary>
        /// Implementation for print(args): Displays a message.
        /// </summary>
        public static object? Print(List<object?> args, IKatroHost host)
        {
            var output = string.Join(" ", args);
            host.Print(output);

            return null;
        }

        /// <summary>
        /// Implementation for len(args): Determines the length of a string.
        /// </summary>
        public static object? Len(List<object?> args, IKatroHost host)
        {
            var firstArgument = args.First();

            if (firstArgument is string str)
                return (double)str.Length;

            throw new Exception("Invalid type for len(), must be string");
        }

        /// <summary>
        /// Implementation for toString(args): Converts a value to a string.
        /// </summary>
        public static object? ToStr(List<object?> args, IKatroHost host)
        {
            var firstArgument = args.First();
            return firstArgument?.ToString();
        }
    }
}
