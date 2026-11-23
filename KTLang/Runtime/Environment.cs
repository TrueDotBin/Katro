using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime
{
    /// <summary>
    /// Manages KTLang's variables.
    /// </summary>
    public class Environment(Environment? enclosing = null)
    {
        private readonly Dictionary<string, object?> _vars = [];

        /// <summary>
        /// The parent scope's environment.
        /// </summary>
        public Environment? Enclosing { get; private set; } = enclosing;

        /// <summary>
        /// Gets a variable by its name.
        /// </summary>
        public object? Get(string name)
        {
            if (_vars.TryGetValue(name, out var value))
                return value;

            if (Enclosing is not null)
                return Enclosing.Get(name);

            throw new Exception($"Undefined variable '{name}'.");
        }

        /// <summary>
        /// Defines a variable.
        /// </summary>
        public void Define(string name, object? value)
            => _vars[name] = value;

        /// <summary>
        /// Sets a variable's value by its name.
        /// </summary>
        public void Assign(string name, object? value)
        {
            if (_vars.ContainsKey(name))
            {
                _vars[name] = value;
                return;
            }

            if (Enclosing is not null)
            {
                Enclosing.Assign(name, value);
                return;
            }

            throw new Exception($"Undefined variable '{name}'.");
        }
    }
}
