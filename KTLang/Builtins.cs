using KTLang.Runtime.Types;
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
        public delegate KTLangType? NativeFunction(List<KTLangType?> args, IHost host);

        /// <summary>
        /// The list of names that resolve to built-in functions.
        /// </summary>
        public static readonly Dictionary<string, NativeFunction> Registry = new()
        {
            { "println", PrintLn },
            { "print", Print },
            { "len", Len },
            { "toString", ToStr },
            { "toNumber", ToNum }
        };

        /// <summary>
        /// Implementation for println(args): Displays a message with a trailing new line.
        /// </summary>
        public static KTLangType? PrintLn(List<KTLangType?> args, IHost host)
        {
            var output = string.Join(" ", args);
            host.PrintLn(output);

            return null;
        }

        /// <summary>
        /// Implementation for print(args): Displays a message.
        /// </summary>
        public static KTLangType? Print(List<KTLangType?> args, IHost host)
        {
            var output = string.Join(" ", args);
            host.Print(output);

            return null;
        }

        /// <summary>
        /// Implementation for len(args): Determines the length of a string or an array.
        /// </summary>
        public static KTLangType? Len(List<KTLangType?> args, IHost host)
        {
            var firstArgument = args.First();

            if (firstArgument is KTLangString str)
                return str.GetLength();

            if (firstArgument is KTLangArray array)
                return array.GetLength();

            throw new Exception("Invalid type for len(), must be string or array");
        }

        /// <summary>
        /// Implementation for toString(args): Converts a value to a string.
        /// </summary>
        public static KTLangType? ToStr(List<KTLangType?> args, IHost _)
        {
            var firstArgument = args.First();

            return new KTLangString(firstArgument?.ToString() ?? "");
        }

        /// <summary>
        /// Implementation for toNumber(args): Converts a value to a number.
        /// </summary>
        public static KTLangType? ToNum(List<KTLangType?> args, IHost _)
        {
            var firstArgument = args.First();
            var stringified = firstArgument?.ToString();

            return new KTLangNumber(double.Parse(stringified ?? "0"));
        }
    }
}
