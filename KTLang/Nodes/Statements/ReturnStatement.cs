using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// An <c>return</c> statement, which returns a value (or nothing)
    /// </summary>
    public class ReturnStatement(Expression? value) : Statement
    {
        /// <summary>
        /// The expression to return.
        /// </summary>
        public Expression? Value { get; } = value;
    }
}
