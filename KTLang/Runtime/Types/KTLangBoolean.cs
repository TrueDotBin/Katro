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

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is bool boolean)
                return Value == boolean;

            if (obj is KTLangBoolean other)
                return Value == other.Value;

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
            => Value ? "true" : "false"; // C#'s ToString() on boolean makes it capital
    }
}
