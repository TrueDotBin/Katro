using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Tokenizer;

namespace KTLang.Runner
{
    /// <summary>
    /// Executes or evaluates each statement or expression.
    /// </summary>
    public class Interpreter
    {
        private Environment _env = new();

        private object? DoBinaryExpression(BinaryExpression bin)
        {
            var left = Evaluate(bin.Left);
            var right = Evaluate(bin.Right);

            switch (bin.Operator.Type)
            {
                case TokenType.Add:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble + rightDouble;

                        if (left is string leftStr && right is string rightStr)
                            return leftStr + rightStr;

                        throw new Exception("Operands must be numbers or strings.");
                    }

                case TokenType.Subtract:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble + rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.Multiply:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble * rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.Divide:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble / rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.Equal:
                    return left?.Equals(right);

                case TokenType.NotEqual:
                    return !left?.Equals(right);

                default:
                    throw new Exception("Invalid operator");
            }
        }

        /// <summary>
        /// Returns the actual value of an expression.
        /// </summary>
        public object? Evaluate(Expression expr)
        {
            if (expr is LiteralExpression literal)
            {
                return literal.Value;
            }
            else if (expr is VariableExpression var)
            {
                var name = var.Name?.RawString;

                if (string.IsNullOrEmpty(name))
                    return null;

                var value = _env.Get(name);

                return value is null ? throw new Exception($"Variable {name} was used before being assigned") : value;
            }
            else if (expr is BinaryExpression bin)
            {
                return DoBinaryExpression(bin);
            }
            else if (expr is CallExpression call)
            {
                if (call.Callee is VariableExpression varExpr)
                {
                    var name = varExpr.Name?.RawString;

                    if (string.IsNullOrEmpty(name))
                        throw new NullReferenceException("Function name was null or empty");

                    if (Builtins.Registry.TryGetValue(name, out var builtin))
                    {
                        var args = new List<object?>();
                        foreach (var arg in call.Args)
                        {
                            var value = Evaluate(arg);
                            args.Add(value);
                        }

                        return builtin(args);
                    }
                }

                throw new Exception("Undefinied function");
            }

            throw new Exception($"Unknown expression type: {expr.GetType().Name}");
        }

        /// <summary>
        /// Executes the provided statement.
        /// </summary>
        public void Execute(Statement statement)
        {
            if (statement is VariableStatement var)
            {
                var name = var.Name?.RawString;

                if (string.IsNullOrEmpty(name))
                    throw new NullReferenceException("Variable name was null or empty");

                object? value = null;

                if (var.Initializer is not null)
                    value = Evaluate(var.Initializer);

                _env.Set(name, value);
            }
            else if (statement is ExpressionStatement expr)
            {
                Evaluate(expr.Expression);
            }
            else if (statement is IfStatement ifStatement)
            {
                var condition = Evaluate(ifStatement.Condition);

                if (condition is bool boolean)
                {
                    if (boolean)
                        Execute(ifStatement.ThenBranch);
                    else if (!boolean && ifStatement.ElseBranch is not null)
                        Execute(ifStatement.ElseBranch);
                }
            }
            else if (statement is BlockStatement block)
            {
                foreach (var blockElement in block.Statements)
                {
                    Execute(blockElement);
                }
            }
        }

        /// <summary>
        /// Executes a list of statements.
        /// </summary>
        /// <param name="statements">The statements to execute.</param>
        public void Interpret(List<Statement> statements)
        {
            foreach (var statement in statements)
            {
                Execute(statement);
            }
        }
    }
}
