using KTLang.Nodes.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// An expression that has been used as a statement.
    /// </summary>
    public class ExpressionStatement(Expression expr) : Statement
    {
        /// <summary>
        /// The expression to evaluate.
        /// </summary>
        public Expression Expression { get; } = expr;
    }
}
