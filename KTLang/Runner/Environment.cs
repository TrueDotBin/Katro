using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runner
{
    /// <summary>
    /// Manages KTLang's variables.
    /// </summary>
    public class Environment
    {
        private static Dictionary<string, object?> _vars = [];

        /// <summary>
        /// Gets a variable by its name.
        /// </summary>
        public object? Get(string name)
            => _vars.TryGetValue(name, out var value) ? value : null;

        /// <summary>
        /// Sets a variable by its name.
        /// </summary>
        public object? Set(string name, object? value)
            => _vars[name] = value;

        /// <summary>
        /// Sets a variable's value by its name, but does not create a new one.
        /// </summary>
        public void Assign(string name, object? value)
        {
            if (_vars.ContainsKey(name))
            {
                _vars[name] = value;
                return;
            }

            throw new Exception($"Undefined variable '{name}'.");
        }
    }
}
