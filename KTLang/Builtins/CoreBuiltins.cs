using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Builtins
{
    /// <summary>
    /// Standard built-in classes.
    /// </summary>
    public static class CoreBuiltins
    {
        /// <summary>
        /// Writes a message to the output with a trailing line break.
        /// </summary>
        public static KTLangType? PrintLn(List<KTLangType?> args, IHost host)
        {
            var text = string.Join(" ", args);
            host.PrintLn(text);

            return null;
        }

        /// <summary>
        /// Writes a message to the output.
        /// </summary>
        public static KTLangType? Print(List<KTLangType?> args, IHost host)
        {
            var text = string.Join(" ", args);
            host.Print(text);

            return null;
        }

        /// <summary>
        /// Returns the length of a string or array.
        /// </summary>
        public static KTLangType? Len(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("len() requires at least one argument");

            var first = args.First();

            if (first is KTLangString str)
                return str.GetLength();

            if (first is KTLangArray array)
                return array.GetLength();

            throw new Exception("Cannot get length of specified type, must be string or array");
        }

        /// <summary>
        /// Converts the provided argument to a string.
        /// </summary>
        public static KTLangType? ToStr(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("toString() requires at least one argument");

            var first = args.First();
            var stringified = first?.ToString();

            if (string.IsNullOrEmpty(stringified))
                throw new Exception("Failed to convert provided argument to string");

            return new KTLangString(stringified);
        }

        /// <summary>
        /// Converts a string to a number.
        /// </summary>
        public static KTLangType? ToNum(List<KTLangType?> args, IHost _)
        {
            if (args.Count == 0)
                throw new Exception("toNumber() requires at least one argument");

            var first = args.First();

            if (first is KTLangString str && double.TryParse(str.Value, out var result))
                return new KTLangNumber(result);

            throw new Exception("Cannot convert specified type to number, must be string");
        }
    }
}
