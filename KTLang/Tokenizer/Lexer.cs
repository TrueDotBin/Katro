using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Tokenizer
{
    /// <summary>
    /// Parses the provided source code into a list of tokens.
    /// </summary>
    public sealed class Lexer(string input)
    {
        private string _input = input;
        private int _position = 0;
        private int _start = 0;
        private int _line = 1;

        private static readonly Dictionary<string, TokenType> _keywords = new()
        {
            { "var", TokenType.Var },
            { "if", TokenType.If },
            { "else", TokenType.Else },
            { "return", TokenType.Return },
            { "fn", TokenType.Fn },
            { "true", TokenType.True },
            { "false", TokenType.False },
            { "while", TokenType.While }
        };

        private Token? ScanToken()
        {
            var c = Advance();

            switch (c)
            {
                case '(':
                    return CreateToken(TokenType.LParen);

                case ')':
                    return CreateToken(TokenType.RParen);

                case '{':
                    return CreateToken(TokenType.LBrace);

                case '}':
                    return CreateToken(TokenType.RBrace);

                case ';':
                    return CreateToken(TokenType.Semicolon);

                case '.':
                    return CreateToken(TokenType.Dot);

                case ',':
                    return CreateToken(TokenType.Comma);

                case '+':
                    return CreateToken(TokenType.Add);

                case '-':
                    return CreateToken(TokenType.Subtract);

                case '/':
                    return CreateToken(TokenType.Divide);

                case '*':
                    return CreateToken(TokenType.Multiply);

                case '!':
                    return CreateToken(MatchNext('=') ? TokenType.NotEqual : TokenType.Exclamation);

                case '=':
                    return CreateToken(MatchNext('=') ? TokenType.Equal : TokenType.Assign);

                case '>':
                    if (MatchNext(' '))
                        while (Peek() != '\n' && !IsAtEnd()) Advance();
                    else
                        return CreateToken(MatchNext('=') ? TokenType.GreaterEqual : TokenType.Greater);

                    break;

                case '<':
                    return CreateToken(MatchNext('=') ? TokenType.LessEqual : TokenType.Less);

                case ' ':
                case '\t':
                case '\r':
                    break;

                case '"':
                    {
                        var value = String();
                        return CreateToken(TokenType.String, value);
                    }

                case '\n':
                    _line++;
                    break;

                default:
                    if (IsDigit(c))
                    {
                        var num = Number();
                        return CreateToken(TokenType.Number, num);
                    }
                    else if (IsAlpha(c))
                    {
                        var (type, value) = Identifier();
                        return CreateToken(type, value);
                    }

                    throw new Exception($"Unexpected character '{c}' on line {_line}");
            }

            return null;
        }

        /// <summary>
        /// Parses the provided source code into a list of tokens.
        /// </summary>
        public List<Token> Parse()
        {
            var tokens = new List<Token>();

            while (!IsAtEnd())
            {
                _start = _position;

                var token = ScanToken();

                if (token is not null)
                    tokens.Add(token);
            }

            var eof = new Token(TokenType.Eof, "", null, _line);
            tokens.Add(eof);

            return tokens;
        }

        private char Advance()
            => _input[_position++];

        private Token CreateToken(TokenType type, object? literal)
        {
            var text = _input.Substring(_start, _position - _start);
            return new Token(type, text, literal, _line);
        }

        private Token CreateToken(TokenType type)
            => CreateToken(type, null);

        private bool IsAtEnd() => _position >= _input.Length;

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return _input[_position];
        }

        private char PeekNext()
        {
            if (_position + 1 >= _input.Length) return '\0';
            return _input[_position + 1];
        }

        private bool MatchNext(char c)
        {
            if (IsAtEnd())
                return false;

            if (_input[_position] != c)
                return false;

            _position++;
            return true;
        }

        private string String()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n')
                    _line++;

                Advance();
            }

            if (IsAtEnd())
                throw new Exception($"Unterminated string on line {_line}");

            Advance();

            string value = _input.Substring(_start + 1, _position - _start - 2);
            return value;
        }

        private double Number()
        {
            while (IsDigit(Peek())) Advance();

            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                Advance();
                while (IsDigit(Peek())) Advance();
            }

            string value = _input.Substring(_start, _position - _start);
            return double.Parse(value);
        }

        private (TokenType Type, object? Value) Identifier()
        {
            while (IsAlphaNumeric(Peek())) Advance();

            string text = _input.Substring(_start, _position - _start);

            if (_keywords.TryGetValue(text, out TokenType type))
            {
                object? value = null;

                if (type == TokenType.True)
                    value = true;
                else if (type == TokenType.False)
                    value = false;

                return (type, value);
            }
            else
            {
                return (TokenType.Identifier, null);
            }
        }

        private bool IsDigit(char c)
            => c >= '0' && c <= '9';

        private bool IsAlpha(char c)
            => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';

        private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsDigit(c);
    }
}
