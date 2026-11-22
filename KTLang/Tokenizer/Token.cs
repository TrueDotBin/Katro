using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Tokenizer
{
    /// <summary>
    /// A single lexical token produced by the tokenizer.
    /// </summary>
    public class Token(TokenType type, string raw, object? literal, int line)
    {
        /// <summary>
        /// The category of this token.
        /// </summary>
        public TokenType Type { get; init; } = type;

        /// <summary>
        /// The exact substring taken from the source code that produced this token.
        /// </summary>
        public string RawString { get; init; } = raw;
        
        /// <summary>
        /// The runtime value of this token, if any.
        /// </summary>
        public object? Literal { get; init; } = literal;

        /// <summary>
        /// The line that produced this token.
        /// </summary>
        public int Line { get; init; } = line;

        /// <inheritdoc/>
        public override string ToString()
            => $"{Type} -> {RawString} ({Literal ?? "null"}) at line {Line}";
    }
}
