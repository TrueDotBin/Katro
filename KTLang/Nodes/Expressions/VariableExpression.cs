using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A reference to a variable by its name.
    /// </summary>
    public class VariableExpression(Token? name) : Expression
    {
        /// <summary>
        /// The token containing the variable's name and source location.
        /// </summary>
        public Token? Name { get; } = name;
    }
}
