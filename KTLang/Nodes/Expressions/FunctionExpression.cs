using KTLang.Nodes.Statements;
using KTLang.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// An expression that holds the function's definition.
    /// </summary>
    public sealed class FunctionExpression(FunctionStatement definition) : Expression
    {
        /// <summary>
        /// The statement that defines the function.
        /// </summary>
        public FunctionStatement Definition { get; } = definition;
    }
}
