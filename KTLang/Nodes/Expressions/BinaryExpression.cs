using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A binary operation.
    /// </summary>
    public class BinaryExpression(Expression left, Token op, Expression right) : Expression
    {
        /// <summary>
        /// The left side of this expression.
        /// </summary>
        public Expression Left { get; } = left;

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
