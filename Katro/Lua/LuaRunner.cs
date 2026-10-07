using Cosmos.Executable.Lua;
using Katro.Logging;
using Katro.Lua.Libraries;
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
            {
                s_interpreter.WorkingDirectory = "/katro";
                RunString("package.path = '/katro/lib/?.lua;/katro/lib/?/init.lua;' .. package.path");
            }

            KatroSystemLibrary.RegisterToLua(s_interpreter);
        }

        /// <summary>
        /// Runs a Lua file.
        /// </summary>
        /// <param name="path">The path of the Lua file.</param>
        /// <param name="args">The arguments to provide to the Lua interpreter.</param>
        public static void RunFile(string path, params string[] args)
        {
            try
            {
                s_interpreter.DoFile(path, args);
            }
            catch (LuaException e)
            {
                LogLuaException(e);
            }
        }

        /// <summary>
        /// Runs a Lua string.
        /// </summary>
        /// <param name="lua">The Lua string to run.</param>
        public static void RunString(string lua)
        {
            try
            {
                s_interpreter.DoString(lua);
            }
            catch (LuaException e)
            {
                LogLuaException(e);
            }
        }

        private static void LogLuaException(LuaException e)
        {
            Logger.Error($"Lua Script Error: {e.Message}");
            Logger.Error(e.LuaStackTrace ?? "No stack trace");
        }
    }
}
