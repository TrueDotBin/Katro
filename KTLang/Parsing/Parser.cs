using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Parsing
{
    /// <summary>
    /// Parses a list of tokens into an Abstract Syntax Tree.
    /// </summary>
    public sealed class Parser(List<Token> tokens)
    {
        private readonly List<Token> _tokens = tokens;
        private int _position = 0;

        /// <summary>
        /// Parses the entire token stream into a list of statements.
        /// </summary>
        /// <returns>A list of <see cref="Statement"/> representing the program.</returns>
        public List<Statement> Parse()
        {
            var statements = new List<Statement>();

            while (!IsAtEnd())
            {
                statements.Add(Declaration());
            }

            return statements;
        }
        
        private Statement Declaration()
        {
            if (Match(TokenType.Var))
                return VariableDecl();

            return Statement();
        }

        private Statement VariableDecl()
        {
            var name = Consume(TokenType.Identifier, "Expected variable name");

            Expression? initializer = null;
            if (Match(TokenType.Assign))
                initializer = Expression();

            Consume(TokenType.Semicolon, "Expected semicolon after variable decleration");
            return new VariableStatement(name, initializer);
        }

        private Statement Statement()
        {
            if (Match(TokenType.If))
                return IfStatement();

            if (Match(TokenType.While))
                return WhileStatement();

            if (Match(TokenType.LBrace))
                return Block();

            return ExpressionStatement();
        }

        private Statement ExpressionStatement()
        {
            var expr = Expression();
            Consume(TokenType.Semicolon, "Expected semicolon after expression");

            return new ExpressionStatement(expr);
        }

        private Statement IfStatement()
        {
            var condition = Expression();
            var thenBranch = Statement();
            Statement? elseBranch = null;

            if (Match(TokenType.Else))
                elseBranch = Statement();

            return new IfStatement(condition, thenBranch, elseBranch);
        }

        private Statement WhileStatement()
        {
            var condition = Expression();
            var body = Statement();

            return new WhileStatement(condition, body);
        }

        private Statement Block()
        {
            var statements = new List<Statement>();

            while (!Check(TokenType.RBrace) && !IsAtEnd())
                statements.Add(Declaration());

            Consume(TokenType.RBrace, "Expected '}' after block");
            return new BlockStatement(statements);
        }

        private Expression Primary()
        {
            if (Match(TokenType.Number, TokenType.String, TokenType.True, TokenType.False))
            {
                var token = Previous();
                return new LiteralExpression(token.Literal);
            }

            if (Match(TokenType.Identifier))
            {
                var token = Previous();
                return new VariableExpression(token);
            }

            if (Match(TokenType.LParen))
            {
                var expr = Expression();
                Consume(TokenType.RParen, "Expected ')' after expression");
                return expr;
            }

            var badToken = Peek();
            throw new Exception($"Expected expression, but got {badToken.Type} ('{badToken.RawString}') at line {badToken.Line}");
        }

        private Expression Expression()
        {
            if (Check(TokenType.Identifier) && PeekNext().Type == TokenType.Assign)
            {
                var name = Advance();
                Advance();

                var value = Expression();

                return new AssignmentExpression(name, value);
            }

            return Equality();
        }

        private Expression Equality()
        {
            var left = Comparison();

            while (Match(TokenType.Equal, TokenType.NotEqual))
            {
                var op = Previous();
                var right = Comparison();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private Expression Comparison()
        {
            var left = Addition();

            while (Match(TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual))
            {
                var op = Previous();
                var right = Addition();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private Expression Addition()
        {
            var left = Multiplication();

            while (Match(TokenType.Add, TokenType.Subtract))
            {
                var op = Previous();
                var right = Multiplication();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private Expression Multiplication()
        {
            var left = Unary();

            while (Match(TokenType.Multiply, TokenType.Divide))
            {
                var op = Previous();
                var right = Unary();
                left = new BinaryExpression(left, op, right);
            }

            return left;
        }

        private Expression Unary()
        {
            if (Match(TokenType.Exclamation, TokenType.Subtract))
            {
                var op = Previous();
                var right = Unary();
                return new UnaryExpression(op, right);
            }

            return Call();
        }

        private Expression Call()
        {
            var expr = Primary();

            while (Match(TokenType.LParen))
            {
                var args = new List<Expression>();

                if (!Check(TokenType.RParen))
                {
                    do
                    {
                        var arg = Expression();
                        args.Add(arg);
                    } while (Match(TokenType.Comma));
                }

                Consume(TokenType.RParen, "Expected ')' after arguments");

                expr = new CallExpression(expr, args);
            }

            return expr;
        }

        private Token Advance()
        {
            if (!IsAtEnd())
                _position++;

            return Previous();
        }

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private bool Check(TokenType type)
        {
            if (IsAtEnd())
                return false;

            return Peek().Type == type;
        }

        private bool IsAtEnd()
            => Peek().Type == TokenType.Eof;

        private Token Peek()
            => _tokens[_position];

        private Token PeekNext()
        {
            if (_position + 1 >= _tokens.Count)
                return Peek();

            return _tokens[_position + 1];
        }

        private Token Previous()
            => _tokens[_position - 1];

        private Token Consume(TokenType type, string message)
        {
            if (Check(type))
                return Advance();

            throw new Exception($"Error at line {Peek().Line}: {message}");
        }
    }
}
