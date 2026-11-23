using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A floating-point number.
    /// </summary>
    public sealed class KTLangNumber(double value) : KTLangType
    {
        /// <summary>
        /// The value of this number.
        /// </summary>
        public double Value { get; } = value;
    }
}
