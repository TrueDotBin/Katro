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

        /// <summary>
        /// Adds the left and right values.
        /// </summary>
        public static KTLangNumber operator +(KTLangNumber left, KTLangNumber right)
            => new(left.Value + right.Value);

        /// <summary>
        /// Subtracts the left and right values.
        /// </summary>
        public static KTLangNumber operator -(KTLangNumber left, KTLangNumber right)
            => new(left.Value - right.Value);

        /// <summary>
        /// Multiplies the left and right values.
        /// </summary>
        public static KTLangNumber operator *(KTLangNumber left, KTLangNumber right)
            => new(left.Value * right.Value);

        /// <summary>
        /// Divides the left and right values.
        /// </summary>
        public static KTLangNumber operator /(KTLangNumber left, KTLangNumber right)
            => new(left.Value / right.Value);

        /// <summary>
        ///  Calculates the remainder of dividing the left and right values.
        /// </summary>
        public static KTLangNumber operator %(KTLangNumber left, KTLangNumber right)
            => new(left.Value % right.Value);

        /// <summary>
        /// Checks if the left side is greater than the right side.
        /// </summary>
        public static bool operator >(KTLangNumber left, KTLangNumber right)
            => left.Value > right.Value;

        /// <summary>
        /// Checks if the right side is greater than the left side.
        /// </summary>
        public static bool operator <(KTLangNumber left, KTLangNumber right)
            => left.Value < right.Value;

        /// <summary>
        /// Checks if the left side is greater than or equal to the right side.
        /// </summary>
        public static bool operator >=(KTLangNumber left, KTLangNumber right)
            => left.Value >= right.Value;

        /// <summary>
        /// Checks if the right side is greater than or equal to the left side.
        /// </summary>
        public static bool operator <=(KTLangNumber left, KTLangNumber right)
            => left.Value <= right.Value;

        /// <summary>
        /// Checks if the right side is equal to the left side.
        /// </summary>
        public static bool operator ==(KTLangNumber left, KTLangNumber right)
            => left.Value == right.Value;

        /// <summary>
        /// Checks if the right side is not equal to the left side.
        /// </summary>
        public static bool operator !=(KTLangNumber left, KTLangNumber right)
            => left.Value != right.Value;
    }
}
