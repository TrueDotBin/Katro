using KTLang.Nodes.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// A list of values enclosed in square brackets.
    /// </summary>
    public class ArrayExpression(List<Expression> values) : Expression
    {
        /// <summary>
        /// The list of expressions that should be evaluated.
        /// </summary>
        public List<Expression> Values { get; } = values;
    }
}
