using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// An <c>if</c> statement, with an optional <c>else</c> branch.
    /// </summary>
    public class IfStatement(Expression condition, Statement thenBranch, Statement? elseBranch) : Statement
    {
        /// <summary>
        /// The condition to evaluate.
        /// </summary>
        public Expression Condition { get; } = condition;

        /// <summary>
        /// The statement to execute if the condition is true.
        /// </summary>
        public Statement ThenBranch { get; } = thenBranch;

        /// <summary>
        /// The statement to execute if the condition is false.
        /// </summary>
        public Statement? ElseBranch { get; } = elseBranch;
    }
}
