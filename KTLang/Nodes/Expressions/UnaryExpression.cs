using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A unary operation.
    /// </summary>
    public class UnaryExpression(Token op, Expression right) : Expression
    {
        /// <summary>
        /// The operator token.
        /// </summary>
        public Token Operator { get; } = op;

        /// <summary>
        /// The right side of this expression.
        /// </summary>
        public Expression Right { get; } = right;
    }
}
