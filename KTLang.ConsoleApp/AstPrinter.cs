using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.ConsoleApp
{
    public static class AstPrinter
    {
        public static void PrintAst(Statement stmt, string indent = "")
        {
            if (stmt is VariableStatement varStmt)
            {
                Console.WriteLine($"{indent}Var: {varStmt.Name.RawString}");
                if (varStmt.Initializer != null) PrintAst(varStmt.Initializer, indent + "  ");
            }
            else if (stmt is ExpressionStatement exprStmt)
            {
                Console.WriteLine($"{indent}Expr:");
                PrintAst(exprStmt.Expression, indent + "  ");
            }
            else if (stmt is IfStatement ifStmt)
            {
                Console.WriteLine($"{indent}If:");
                Console.WriteLine($"{indent}  Condition:");
                PrintAst(ifStmt.Condition, indent + "    ");
                Console.WriteLine($"{indent}  Then:");
                PrintAst(ifStmt.ThenBranch, indent + "    ");
                if (ifStmt.ElseBranch != null)
                {
                    Console.WriteLine($"{indent}  Else:");
                    PrintAst(ifStmt.ElseBranch, indent + "    ");
                }
            }
            else if (stmt is BlockStatement blockStmt)
            {
                Console.WriteLine($"{indent}Block:");
                foreach (var s in blockStmt.Statements) PrintAst(s, indent + "  ");
            }
            else if (stmt is WhileStatement whiteStmt)
            {
                Console.WriteLine($"{indent}While:");
                Console.WriteLine($"{indent}  Condition:");
                PrintAst(whiteStmt.Condition, indent + "    ");
                Console.WriteLine($"{indent}  Do:");
                PrintAst(whiteStmt.Body, indent + "    ");
            }
            else if (stmt is FunctionStatement funcStmt)
            {
                Console.WriteLine($"{indent}Function:");
                Console.WriteLine($"{indent}  Parameters:");

                foreach (var param in funcStmt.Parameters)
                {
                    Console.WriteLine($"{indent}    {param.RawString}");
                }

                Console.WriteLine($"{indent}  Do:");
                PrintAst(funcStmt.Body, indent + "    ");
            }
            else if (stmt is ReturnStatement returnStmt)
            {
                if (returnStmt.Value is null)
                {
                    Console.WriteLine($"{indent}Return");
                    return;
                }

                Console.WriteLine($"{indent}Return:");
                PrintAst(returnStmt.Value, indent + "    ");
            }
        }

        public static void PrintAst(Expression expr, string indent = "")
        {
            if (expr is LiteralExpression lit)
            {
                Console.WriteLine($"{indent}Literal: {lit.Value}");
            }
            else if (expr is VariableExpression var)
            {
                Console.WriteLine($"{indent}Var: {var.Name?.RawString}");
            }
            else if (expr is BinaryExpression bin)
            {
                Console.WriteLine($"{indent}Binary: {bin.Operator.RawString}");
                PrintAst(bin.Left, indent + "  ");
                PrintAst(bin.Right, indent + "  ");
            }
            else if (expr is CallExpression call)
            {
                Console.WriteLine($"{indent}Call:");
                PrintAst(call.Callee, indent + "  ");
                foreach (var arg in call.Args) PrintAst(arg, indent + "    ");
            }
            else if (expr is AssignmentExpression assign)
            {
                Console.WriteLine($"{indent}Assign: {assign.Name?.RawString}");
                PrintAst(assign.Value, indent + "    ");
            }
            else if (expr is ArrayExpression array)
            {
                Console.WriteLine($"{indent}Array:");
                foreach (var val in array.Values) PrintAst(val, indent + "  ");
            }
            else if (expr is MemberExpression member)
            {
                Console.WriteLine($"{indent}Member: {member.Name?.RawString}");
                PrintAst(member.Target, indent + "  ");
            }
        }
    }
}
