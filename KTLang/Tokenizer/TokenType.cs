using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Tokenizer
{
    /// <summary>
    /// A token's type, e.g., keyword or string literal.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// (
        /// </summary>
        LParen,

        /// <summary>
        /// )
        /// </summary>
        RParen,

        /// <summary>
        /// {
        /// </summary>
        LBrace,
        
        /// <summary>
        /// }
        /// </summary>
        RBrace,

        /// <summary>
        /// ,
        /// </summary>
        Comma,

        /// <summary>
        /// .
        /// </summary>
        Dot,

        /// <summary>
        /// ;
        /// </summary>
        Semicolon,

        /// <summary>
        /// +
        /// </summary>
        Add,

        /// <summary>
        /// -
        /// </summary>
        Subtract,

        /// <summary>
        /// *
        /// </summary>
        Multiply,

        /// <summary>
        /// /
        /// </summary>
        Divide,

        /// <summary>
        /// %
        /// </summary>
        Modulo,

        /// <summary>
        /// !
        /// </summary>
        Exclamation,

        /// <summary>
        /// !=
        /// </summary>
        NotEqual,

        /// <summary>
        /// =
        /// </summary>
        Assign,

        /// <summary>
        /// ==
        /// </summary>
        Equal,

        /// <summary>
        /// >
        /// </summary>
        Greater,

        /// <summary>
        /// >=
        /// </summary>
        GreaterEqual,

        /// <summary>
        /// <
        /// </summary>
        Less,

        /// <summary>
        /// <=
        /// </summary>
        LessEqual,

        /// <summary>
        /// Function/variable name.
        /// </summary>
        Identifier,

        /// <summary>
        /// String literal.
        /// </summary>
        String,

        /// <summary>
        /// Number (123, 123.5)
        /// </summary>
        Number,

        /// <summary>
        /// Truthy value.
        /// </summary>
        True,
        
        /// <summary>
        /// Falsy value.
        /// </summary>
        False,

        /// <summary>
        /// Variable keyword
        /// </summary>
        Var,
        If, Else,
        While, For,
        Fn, Return,
        Println,

        /// <summary>
        /// The end of the file.
        /// </summary>
        Eof
    }
}
