using Katro.Output;
using KTLang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Scripting
{
    /// <summary>
    /// The host that implements built-in functions.
    /// </summary>
    public sealed class KatroHost : IHost
    {
        /// <inheritdoc/>
        public void PrintLn(string text)
        {
            Terminal.WriteLine(text);
        }

        /// <inheritdoc/>
        public void Print(string text)
        {
            Terminal.Write(text);
        }
    }
}
