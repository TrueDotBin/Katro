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
        /// <summary>
        /// The value of this character sequence.
        /// </summary>
        public string Value { get; } = value;

        /// <summary>
        /// Gets the length of this sequence.
        /// </summary>
        public KTLangNumber GetLength()
            => new(Value.Length);

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is string str)
                return Value == str;

            if (obj is KTLangString other)
                return Value == other.Value;

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString()
            => Value;

        /// <summary>
        /// Concatenates the left side with the right side.
        /// </summary>
        public static KTLangString operator +(KTLangString left, KTLangString right)
            => new(left.Value + right.Value);

        /// <inheritdoc/>
        public override KTLangType? GetMember(string member)
        {
            return member switch
            {
                "length" => GetLength(),
                _ => throw new Exception($"Invalid member name '{member}'")
            };
        }
    }
}
