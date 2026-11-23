using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Tokenizer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KTLang.Runner
{
    /// <summary>
    /// Executes or evaluates each statement or expression.
    /// </summary>
    public class Interpreter(IHost host)
    {
        private IHost _host = host;
        private Environment _env = new();

        /// <summary>
        /// Returns the actual value of an expression.
        /// </summary>
        public object? Evaluate(Expression expr)
        {
            if (expr is LiteralExpression literal)
                return literal.Value;

            if (expr is VariableExpression var)
                return EvalVarExpr(var);

            if (expr is BinaryExpression bin)
                return EvalBinExpr(bin);

            if (expr is CallExpression call)
                return EvalCallExpr(call);

            if (expr is AssignmentExpression assign)
                return EvalAssignExpr(assign);

            throw new Exception($"Unknown expression type: {expr.GetType().Name}");
        }

        /// <summary>
        /// Executes the provided statement.
        /// </summary>
        public void Execute(Statement statement)
        {
            if (statement is VariableStatement var)
            {
                ExecVarStatement(var);
                return;
            }

            if (statement is ExpressionStatement expr)
            {
                Evaluate(expr.Expression);
                return;
            }

            if (statement is IfStatement ifStatement)
            {
                ExecIfStatement(ifStatement);
                return;
            }

            if (statement is BlockStatement block)
            {
                ExecBlockStatement(block);
                return;
            }

            if (statement is WhileStatement whileStatement)
            {
                ExecWhileStatement(whileStatement);
                return;
            }

            throw new Exception($"Unknown statement type: {statement.GetType().Name}");
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

        private object? EvalBinExpr(BinaryExpression bin)
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

                case TokenType.Greater:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble > rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.GreaterEqual:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble >= rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.Less:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble < rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.LessEqual:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble <= rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                case TokenType.Modulo:
                    {
                        if (left is double leftDouble && right is double rightDouble)
                            return leftDouble % rightDouble;

                        throw new Exception("Operands must be numbers.");
                    }

                default:
                    throw new Exception("Invalid operator");
            }
        }

        private object? EvalVarExpr(VariableExpression var)
        {
            var name = var.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            var value = _env.Get(name);

            return value is null ? throw new Exception($"Variable {name} was used before being assigned") : value;
        }

        private object? EvalCallExpr(CallExpression call)
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

                    return builtin(args, _host);
                }
            }

            throw new Exception("Undefinied function");
        }

        private object? EvalAssignExpr(AssignmentExpression assign)
        {
            var name = assign.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            var value = Evaluate(assign.Value);
            _env.Assign(name, value);

            return value;
        }

        private void ExecVarStatement(VariableStatement var)
        {
            var name = var.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Variable name was null or empty");

            object? value = null;

            if (var.Initializer is not null)
                value = Evaluate(var.Initializer);

            _env.Define(name, value);
        }

        private void ExecBlockStatement(BlockStatement block)
        {
            var previous = _env;

            try
            {
                _env = new Environment(previous);

                foreach (var statement in block.Statements)
                {
                    Execute(statement);
                }
            }
            finally
            {
                _env = previous;
            }
        }

        private void ExecIfStatement(IfStatement ifStatement)
        {
            var condition = Evaluate(ifStatement.Condition);
            var truthy = IsTruthy(condition);

            if (truthy)
                Execute(ifStatement.ThenBranch);
            else if (ifStatement.ElseBranch is not null)
                Execute(ifStatement.ElseBranch);
        }

        private void ExecWhileStatement(WhileStatement whileStatement)
        {
            while (IsTruthy(Evaluate(whileStatement.Condition)))
            {
                Execute(whileStatement.Body);
            }
        }

        private bool IsTruthy(object? value)
        {
            if (value is null)
                return false;

            if (value is bool boolean)
                return boolean;

            throw new Exception($"Value must evaluate to a boolean");
        }
    }
}
