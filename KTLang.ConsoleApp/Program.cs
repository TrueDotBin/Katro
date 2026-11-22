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
        }

        public static void PrintAst(Expression expr, string indent = "")
        {
            if (expr is LiteralExpression lit)
                Console.WriteLine($"{indent}Literal: {lit.Value}");
            else if (expr is VariableExpression var)
                Console.WriteLine($"{indent}Var: {var.Name?.RawString}");
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
        }

        public static void Main()
        {
            try
            {
                var source = """
                > This is a comment, it is ignored by the interpreter.

                var a = 5;

                println(a);

                if 2 + 2 == 4
                {
                    println("Math is working correctly!");
                }
                else
                {
                    println("Math is NOT working correctly :(");
                }

                var exampleString = "hello world";
                var strLength = len(exampleString);

                println("Sample string is", exampleString);
                println("String length is", strLength);

                var stringifiedA = toString(a);
                var concatTest = stringifiedA + "Hello";
                println(concatTest);

                var numberified = toNumber(stringifiedA);
                var result = numberified + 500;
                println(result);
                """;

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
