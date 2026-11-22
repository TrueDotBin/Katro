using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// An <c>while</c> statement, which runs while the condition is true.
    /// </summary>
    public class WhileStatement(Expression condition, Statement body) : Statement
    {
        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        public Expression Condition { get; } = condition;

        /// <summary>
        /// The code that gets executed while the condition is true.
        /// </summary>
        public Statement Body { get; } = body;
    }
}
