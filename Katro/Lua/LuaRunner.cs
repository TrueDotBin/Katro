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
            SetupPackagePathForInterpreter(s_interpreter);

            KatroSystemLibrary.RegisterToLua(s_interpreter);
        }

        private static void SetupPackagePathForInterpreter(LuaInterpreter interpreter)
            => interpreter.DoString("package.path = '/katro/lib/?.lua;' .. package.path");

        /// <summary>
        /// Runs a Lua file.
        /// </summary>
        /// <param name="path">The path of the Lua file.</param>
        /// <param name="isolate">Whether to use a fresh interpreter instead of using the global one.</param>
        /// <param name="args">The arguments to provide to the Lua interpreter.</param>
        public static void RunFile(string path, bool isolate = false, params string[] args)
        {
            try
            {
                var interpreter = isolate ? new LuaInterpreter() : s_interpreter;

                if (isolate)
                {
                    SetupPackagePathForInterpreter(interpreter);
                    KatroSystemLibrary.RegisterToLua(interpreter);
                }

                interpreter.DoFile(path, args);
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
        /// <param name="isolate">Whether to use a fresh interpreter instead of using the global one.</param>
        public static void RunString(string lua, bool isolate = false)
        {
            try
            {
                var interpreter = isolate ? new LuaInterpreter() : s_interpreter;

                if (isolate)
                {
                    SetupPackagePathForInterpreter(interpreter);
                    KatroSystemLibrary.RegisterToLua(interpreter);
                }

                interpreter.DoString(lua);
            }
            catch (LuaException e)
            {
                LogLuaException(e);
            }
        }

        private static void LogLuaException(LuaException e)
        {
            Logger.Error($"Lua Script Error: {e.Message}");

            if (!string.IsNullOrEmpty(e.LuaStackTrace))
                Logger.Error(e.LuaStackTrace);
        }
    }
}