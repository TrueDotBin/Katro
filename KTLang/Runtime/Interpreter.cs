using KTLang.Builtins;
using KTLang.Exceptions;
using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Runtime.Types;
using KTLang.Tokenizer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KTLang.Runtime
{
    /// <summary>
    /// Executes or evaluates each statement or expression.
    /// </summary>
    public class Interpreter(IHost host)
    {
        private IHost _host = host;
        private KTLangEnvironment _env = new();

        /// <summary>
        /// Returns the actual value of an expression.
        /// </summary>
        public KTLangType? Evaluate(Expression expr)
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

            if (expr is ArrayExpression array)
                return EvalArrayExpr(array);

            if (expr is MemberExpression member)
                return EvalMemberExpr(member);

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

            if (statement is FunctionStatement func)
            {
                ExecFuncStatement(func);
                return;
            }

            if (statement is ReturnStatement ret)
            {
                ExecReturnStatement(ret);
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

        /// <summary>
        /// Executes a block statement.
        /// </summary>
        public void ExecuteBlock(BlockStatement block, KTLangEnvironment environment)
        {
            var previous = _env;
            try
            {
                _env = environment;

                foreach (var statement in block.Statements)
                    Execute(statement);
            }
            finally
            {
                _env = previous;
            }
        }

        private KTLangType? EvalBinExpr(BinaryExpression bin)
        {
            var left = Evaluate(bin.Left);
            var right = Evaluate(bin.Right);

            switch (bin.Operator.Type)
            {
                case TokenType.Add:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return lNum + rNum;

                        if (left is KTLangString lStr && right is KTLangString rStr)
                            return lStr + rStr;

                        throw new Exception("Operands must be numbers or strings");
                    }

                case TokenType.Subtract:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return lNum - rNum;

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.Multiply:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return lNum * rNum;

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.Divide:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return lNum / rNum;

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.Equal:
                    return new KTLangBoolean(left?.Equals(right) ?? false);

                case TokenType.NotEqual:
                    return new KTLangBoolean(!left?.Equals(right) ?? false);

                case TokenType.Greater:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return new KTLangBoolean(lNum > rNum);

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.GreaterEqual:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return new KTLangBoolean(lNum.Value >= rNum.Value);

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.Less:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return new KTLangBoolean(lNum < rNum);

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.LessEqual:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return new KTLangBoolean(lNum <= rNum);

                        throw new Exception("Operands must be numbers");
                    }

                case TokenType.Modulo:
                    {
                        if (left is KTLangNumber lNum && right is KTLangNumber rNum)
                            return lNum % rNum;

                        throw new Exception("Operands must be numbers");
                    }

                default:
                    throw new Exception("Invalid operator");
            }
        }

        private KTLangType? EvalVarExpr(VariableExpression var)
        {
            var name = var.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            var value = _env.Get(name);

            return value is null ? throw new Exception($"Variable {name} was used before being assigned") : value;
        }

        private KTLangType? EvalMemberMethodExpr(CallExpression call, MemberExpression member)
        {
            var target = Evaluate(member.Target) ?? throw new Exception("Cannot call method on null");
            var name = member.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            var args = new List<KTLangType?>();

            foreach (var arg in call.Args)
                args.Add(Evaluate(arg));

            return target.CallMethod(name, args);
        }

        private KTLangType? EvalMemberExpr(MemberExpression member)
        {
            var target = Evaluate(member.Target) ?? throw new Exception("Cannot access property on null");
            var name = member.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            return target.GetMember(name);
        }

        private KTLangType? EvalCallExpr(CallExpression call)
        {
            if (call.Callee is MemberExpression member)
                return EvalMemberMethodExpr(call, member);

            if (call.Callee is VariableExpression varExpr)
            {
                var name = varExpr.Name?.RawString;

                if (string.IsNullOrEmpty(name))
                    throw new NullReferenceException("Function name was null or empty");

                var args = new List<KTLangType?>();
                foreach (var arg in call.Args)
                {
                    var value = Evaluate(arg);
                    args.Add(value);
                }

                if (BuiltinsRegistry.Registry.TryGetValue(name, out var builtin))
                    return builtin(args, _host);

                var variable = _env.Get(name);

                if (variable is KTLangFunction func)
                    return func.Call(this, args, _host);

                throw new Exception($"Undefinied function '{name}'");
            }

            throw new Exception("Undefinied function");
        }

        private KTLangType? EvalAssignExpr(AssignmentExpression assign)
        {
            var name = assign.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return null;

            var value = Evaluate(assign.Value);
            _env.Assign(name, value);

            return value;
        }

        private KTLangType? EvalArrayExpr(ArrayExpression array)
        {
            var list = new KTLangArray();

            foreach (var value in array.Values)
                list.Push(Evaluate(value));

            return list;
        }

        private void ExecReturnStatement(ReturnStatement ret)
        {
            KTLangType? value = null;

            if (ret.Value is not null)
                value = Evaluate(ret.Value);

            throw new ReturnException(value);
        }

        private void ExecFuncStatement(FunctionStatement func)
        {
            var expr = new FunctionExpression(func);
            var function = new KTLangFunction(expr, _env);

            var name = func.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                return;

            _env.Define(name, function);
        }

        private void ExecVarStatement(VariableStatement var)
        {
            var name = var.Name?.RawString;

            if (string.IsNullOrEmpty(name))
                throw new NullReferenceException("Variable name was null or empty");

            KTLangType? value = null;

            if (var.Initializer is not null)
                value = Evaluate(var.Initializer);

            _env.Define(name, value);
        }

        private void ExecBlockStatement(BlockStatement block)
        {
            var previous = _env;

            try
            {
                _env = new KTLangEnvironment(previous);

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

        private bool IsTruthy(KTLangType? value)
        {
            if (value is null)
                return false;

            if (value is KTLangBoolean boolean)
                return boolean.Value;

            throw new Exception($"Value must evaluate to a boolean");
        }
    }
}
