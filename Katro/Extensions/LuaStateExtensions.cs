using Cosmos.Executable.Lua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Extensions
{
    /// <summary>
    /// Extensions for a Lua state.
    /// </summary>
    public static class LuaStateExtensions
    {
        /// <summary>
        /// Creates a new string table and pushes it to the stack.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        /// <param name="list">The list to push.</param>
        public static void PushStringTable(this ILuaState state, IEnumerable<string> list )
        {
            var len = list.Count();
            state.CreateTable(len, 0);

            for (int i = 0; i < len; i++)
            {
                var item = list.ElementAt(i);
                state.PushString(item);

                state.RawSetI(-2, i + 1);
            }
        }
    }
}
