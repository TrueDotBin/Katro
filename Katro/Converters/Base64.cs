using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Converters
{
    /// <summary>
    /// Encodes and decodes Base64.
    /// </summary>
    public static class Base64
    {
        /// <summary>
        /// Encodes the provided byte array to a Base64 string.
        /// </summary>
        /// <param name="value">The value that should be encoded.</param>
        public static string Encode(byte[] value)
            => Convert.ToBase64String(value);

        /// <summary>
        /// Decodes the provided Base64 string to a byte array.
        /// </summary>
        /// <param name="value">The value that should be decoded.</param>
        public static byte[] DecodeBytes(string value)
            => Convert.FromBase64String(value);

        /// <summary>
        /// Encodes the provided string to a Base64 string.
        /// </summary>
        /// <param name="value">The value that should be encoded.</param>
        public static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Encode(bytes);
        }

        /// <summary>
        /// Decodes the provided Base64 string to a readable string.
        /// </summary>
        /// <param name="value">The value that should be decoded.</param>
        public static string DecodeString(string value)
        {
            var decoded = DecodeBytes(value);
            return Encoding.UTF8.GetString(decoded);
        }
    }
}
