using Cosmos.Executable.Lua;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Lua.Libraries
{
    /// <summary>
    /// The Katro system library for Lua.
    /// </summary>
    public static class KatroSystemLibrary
    {
        private static void RegisterCSharpFunction(ILuaState lua, CSharpFunctionDelegate func, string name)
        {
            lua.PushCSharpFunction(func);
            lua.SetGlobal(name);
        }

        /// <summary>
        /// Registers the Katro system library to Lua.
        /// </summary>
        /// <param name="lua">The Lua interpreter to add the library to.</param>
        public static void RegisterToLua(LuaInterpreter lua)
        {
            var state = lua.State;

            RegisterCSharpFunction(state, Log, "katro_log");
            RegisterCSharpFunction(state, RunShellCommand, "katro_runsh");
        }

        /// <summary>
        /// Runs a shell command.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int RunShellCommand(ILuaState state)
        {
            var name = state.L_CheckString(1);
            string args = "";

            if (!state.IsNoneOrNil(2))
                args = state.L_CheckString(2);

            var command = ArgumentParser.Parse($"{name} {args}");

            if (command == null)
            {
                state.L_Error("command not found: {0}", name);
                return 0;
            }

            state.PushInteger(command.Run());
            return 1;
        }

        /// <summary>
        /// Logs messages to Katro.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int Log(ILuaState state)
        {
            var level = state.L_CheckString(1).ToLower().Trim();
            var message = state.L_ToString(2);

            switch (level)
            {
                case "info" or "information":
                    Logger.Info(message);
                    break;

                case "warn" or "warning":
                    Logger.Warn(message);
                    break;

                case "success" or "ok":
                    Logger.Ok(message);
                    break;

                case "error":
                    Logger.Error(message);
                    break;

                default:
                    state.L_Error("invalid log level: {0}", level);
                    break;
            }

            return 0;
        }
    }
}