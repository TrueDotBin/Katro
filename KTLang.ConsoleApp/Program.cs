using KTLang;
using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Parsing;
using KTLang.Runner;
using KTLang.Tokenizer;

namespace KTLang.ConsoleApp
{
    public class Program
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
        }

        public static void Main()
        {
            try
            {
                var source = File.ReadAllText("TestScript.ktl");
                Console.WriteLine("[Lexer] Tokenizing input");

                var lexer = new Lexer(source);
                var tokens = lexer.Parse();

                Console.WriteLine($"[Lexer] {tokens.Count} token(s)");

                foreach (var token in tokens)
                {
                    Console.WriteLine(token);
                }

                Console.WriteLine("[Parser] Parsing tokens");

                var parser = new Parser(tokens);
                var statements = parser.Parse();

                Console.WriteLine($"[Parser] {statements.Count} statement(s)");
                foreach (var statement in statements)
                {
                    PrintAst(statement);
                }

                Console.Write("Execute script?");
                Console.ReadKey(true);
                Console.Clear();

                var host = new SampleHost();
                var interpreter = new Interpreter(host);
                interpreter.Interpret(statements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
