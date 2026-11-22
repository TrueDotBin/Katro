using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// A variable declaration.
    /// </summary>
    public class VariableStatement(Token name, Expression? initializer) : Statement
    {
        /// <summary>
        /// The token containing the variable's name.
        /// </summary>
        public Token Name { get; } = name;

        /// <summary>
        /// The expression to evaluate.
        /// </summary>
        public Expression? Initializer { get; } = initializer;
    }
}
