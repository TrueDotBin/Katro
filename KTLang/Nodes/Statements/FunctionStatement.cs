using KTLang.Nodes.Expressions;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Statements
{
    /// <summary>
    /// A definition of a function.
    /// </summary>
    public class FunctionStatement(Token name, List<Token> parameters, BlockStatement body) : Statement
    {
        /// <summary>
        /// The token containing the name of this function.
        /// </summary>
        public Token Name { get; } = name;

        /// <summary>
        /// The list of parameters that this function needs.
        /// </summary>
        public List<Token> Parameters { get; } = parameters;

        /// <summary>
        /// The block statement to execute when this function is called.
        /// </summary>
        public BlockStatement Body { get; } = body;
    }
}
