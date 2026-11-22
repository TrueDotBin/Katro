using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A literal value directly in the source code.
    /// </summary>
    public class LiteralExpression(object? value) : Expression
    {
        /// <summary>
        /// The actual runtime value.
        /// </summary>
        public object? Value { get; } = value;
    }
}
