using KTLang;
using KTLang.Parsing;
using KTLang.Runner;
using KTLang.Tokenizer;
using System;
using System.Collections.Generic;
using System.Text;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.Scripting
{
    /// <summary>
    /// Executes KTLang scripts.
    /// </summary>
    public static class KTLang
    {
        private static readonly Logger _logger = new("KTLang");

        /// <summary>
        /// Runs a script from the specified string.
        /// </summary>
        /// <param name="script">The script that should be interpreted.</param>
        /// <param name="host">The Katro host that implements functions.</param>
        public static void RunString(string script, IKatroHost host)
        {
            try
            {
                var lexer = new Lexer(script);
                var tokens = lexer.Parse();

                var parser = new Parser(tokens);
                var statements = parser.Parse();

                var interpreter = new Interpreter(host);
                interpreter.Interpret(statements);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }
    }
}
