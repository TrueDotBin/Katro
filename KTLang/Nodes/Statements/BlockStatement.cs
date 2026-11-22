using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// A block enclosed in curly braces.
    /// </summary>
    public class BlockStatement(List<Statement> statements) : Statement
    {
        /// <summary>
        /// The list of statements inside this block.
        /// </summary>
        public List<Statement> Statements { get; } = statements;
    }
}
