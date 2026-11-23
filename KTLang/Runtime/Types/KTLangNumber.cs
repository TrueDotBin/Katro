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

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is double dbl)
                return Value == dbl;

            if (obj is KTLangNumber other)
                return Value == other.Value;

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
            => Value.ToString();
    }
}
