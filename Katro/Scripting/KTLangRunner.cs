using KTLang;
using KTLang.Parsing;
using KTLang.Runtime;
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
    public static class KTLangRunner
    {
        private static IHost _host;
        private static Interpreter _interpreter;
        private static readonly Logger _logger = new("KTLang");

        /// <summary>
        /// Whether KTLang has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Initializes the KTLang runner.
        /// </summary>
        /// <param name="host">The Katro host that implements functions.</param>
        public static void Initialize(IHost host)
        {
            if (Initialized)
                return;

            _host = host;
            _interpreter = new Interpreter(_host);
            Initialized = true;
        }

        /// <summary>
        /// Resets the interpreter's environment.
        /// </summary>
        public static void Reinitialize()
        {
            _interpreter = new Interpreter(_host);
        }

        /// <summary>
        /// Runs a script from the specified string.
        /// </summary>
        /// <param name="script">The script that should be interpreted.</param>
        public static void RunString(string script)
        {
            if (!Initialized)
                return;

            try
            {
                var lexer = new Lexer(script);
                var tokens = lexer.Parse();

                var parser = new Parser(tokens);
                var statements = parser.Parse();

                _interpreter.Interpret(statements);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }
    }
}
