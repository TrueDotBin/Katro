using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A sequence of characters. 
    /// </summary>
    public sealed class KTLangString(string value) : KTLangType
    {
        private readonly string _value = value;

        /// <summary>
        /// Gets the length of this sequence.
        /// </summary>
        public int GetLength()
            => _value.Length;
    }
}
