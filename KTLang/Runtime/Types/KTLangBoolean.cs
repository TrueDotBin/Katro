using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A value that either represents true, or false.
    /// </summary>
    public sealed class KTLangBoolean(bool value) : KTLangType
    {
        /// <summary>
        /// The value of this boolean.
        /// </summary>
        public bool Value { get; } = value;
    }
}
