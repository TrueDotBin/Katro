using KTLang.Exceptions;
using KTLang.Nodes.Expressions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A definition of a function.
    /// </summary>
    public sealed class KTLangFunction(FunctionExpression function, KTLangEnvironment closure) : KTLangType
    {
        /// <summary>
        /// The function's expression that gets resolved when the function is called.
        /// </summary>
        public FunctionExpression Function { get; } = function;

        /// <summary>
        /// The scope where the function was defined.
        /// </summary>
        public KTLangEnvironment Closure { get; } = closure;

        /// <summary>
        /// Calls the function.
        /// </summary>
        public KTLangType? Call(Interpreter interpreter, List<KTLangType?> args, IHost host)
        {
            if (args.Count != Function.Definition.Parameters.Count)
                throw new Exception($"Expected {Function.Definition.Parameters.Count} arguments but got {args.Count}");

            var env = new KTLangEnvironment(Closure);

            for (int i = 0; i < Function.Definition.Parameters.Count; i++)
            {
                var parameter = Function.Definition.Parameters[i].RawString;
                var arg = args[i];

                env.Define(parameter, arg);
            }

            try
            {
                interpreter.ExecuteBlock(Function.Definition.Body, env);
            }
            catch (ReturnException returnEx)
            {
                return returnEx.Value;
            }

            return null;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"function {Function.Definition.Name?.RawString}";
    }
}
