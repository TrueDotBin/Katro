using KTLang;
using KTLang.Tokenizer;

namespace KTLang.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                var source = """
                > This is a comment, it is ignored by the interpreter.

                fn greet(name) {
                    println("Hello,", name)
                }

                var a = 5;
                var b = 10;
                var c = a + b;

                println("a =", a);
                println("b =", b);
                println("a + b =", c);

                if 2 + 2 == 4
                {
                    println("Math is working correctly!")
                }
                else
                {
                    println("Math is NOT working correctly :(")
                }
                """;

                Console.WriteLine("[Lexer] Tokenizing input");

                var lexer = new Lexer(source);
                var tokens = lexer.Parse();

                Console.WriteLine($"[Lexer] {tokens.Count} token(s)");

                foreach (var token in tokens)
                {
                    Console.WriteLine(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
