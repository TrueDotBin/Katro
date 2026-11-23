using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Nodes.Expressions
{
    /// <summary>
    /// Accessing a member of a type.
    /// </summary>
    public class MemberExpression(Expression target, Token? name) : Expression
    {
        /// <summary>
        /// The target expression that holds the member.
        /// </summary>
        public Expression Target { get; } = target;

        /// <summary>
        /// The token containing the name of the member.
        /// </summary>
        public Token? Name { get; } = name;
    }
}
