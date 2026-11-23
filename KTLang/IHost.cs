using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang
{
    /// <summary>
    /// Stubs for methods that KTLang uses, including output I/O, file I/O, etc.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Writes the specified text to the output, with a trailing new line.
        /// </summary>
        void PrintLn(string text);

        /// <summary>
        /// Writes the specified text to the output.
        /// </summary>
        void Print(string text);
    }
}
