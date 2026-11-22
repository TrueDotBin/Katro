using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// An assignemt to a variable.
    /// </summary>
    public class AssignmentExpression(Token? name, Expression value) : Expression
    {
        /// <summary>
        /// The token containing the target variable's name.
        /// </summary>
        public Token? Name { get; } = name;

        /// <summary>
        /// The value that should be assigned.
        /// </summary>
        public Expression Value { get; } = value;
    }
}
