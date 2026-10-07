using Cosmos.Executable.Lua;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Lua
{
    /// <summary>
    /// Runs Lua scripts. Uses <see cref="LuaInterpreter"/> under the hood.
    /// </summary>
    public static class LuaRunner
    {
        private static LuaInterpreter s_interpreter;

        static LuaRunner()
        {
            s_interpreter = new LuaInterpreter();

            if (!Kernel.DontUseFilesystem)
                s_interpreter.WorkingDirectory = "/katro";
        }

        /// <summary>
        /// Runs a Lua file.
        /// </summary>
        /// <param name="path">The path of the Lua file.</param>
        /// <param name="args">The arguments to provide to the Lua interpreter.</param>
        public static void RunFile(string path, params string[] args)
            => s_interpreter.DoFile(path, args);

        /// <summary>
        /// Runs a Lua string.
        /// </summary>
        /// <param name="lua">The Lua string to run.</param>
        public static void RunString(string lua)
            => s_interpreter.DoString(lua);
    }
}
