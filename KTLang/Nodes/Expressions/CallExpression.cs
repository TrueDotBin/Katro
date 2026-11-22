using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A call to a function.
    /// </summary>
    public class CallExpression(Expression callee, List<Expression> args) : Expression
    {
        /// <summary>
        /// The expression that called this function.
        /// </summary>
        public Expression Callee { get; } = callee;

        /// <summary>
        /// The list of expressions passed to the function as arguments.
        /// </summary>
        public List<Expression> Args { get; } = args;
    }
}
