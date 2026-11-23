using KTLang;
using KTLang.Nodes.Expressions;
using KTLang.Nodes.Statements;
using KTLang.Parsing;
using KTLang.Runtime;
using KTLang.Tokenizer;

namespace KTLang.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var debug = args.Contains("--debug");


                var source = File.ReadAllText("TestScript.ktl");
                Console.WriteLine("[Lexer] Tokenizing input");

                var lexer = new Lexer(source);
                var tokens = lexer.Parse();

                Console.WriteLine($"[Lexer] {tokens.Count} token(s)");

                if (debug)
                {
                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token);
                    }
                }

                Console.WriteLine("[Parser] Parsing tokens");

                var parser = new Parser(tokens);
                var statements = parser.Parse();

                Console.WriteLine($"[Parser] {statements.Count} statement(s)");
                
                if (debug)
                {
                    foreach (var statement in statements)
                    {
                        AstPrinter.PrintAst(statement);
                    }
                }

                Console.WriteLine("Press any key to execute the script");
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
