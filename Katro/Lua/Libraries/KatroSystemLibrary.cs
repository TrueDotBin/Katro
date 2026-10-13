using Cosmos.Executable.Lua;
using Katro.Logging;
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
            RegisterCSharpFunction(state, GetCwd, "katro_get_cwd");
            RegisterCSharpFunction(state, ReadString, "katro_read_string");
            RegisterCSharpFunction(state, SetColor, "katro_set_color");
            RegisterCSharpFunction(state, ResetColor, "katro_reset_color");
            RegisterCSharpFunction(state, Write, "katro_write");
            RegisterCSharpFunction(state, WriteLn, "katro_writeln");
        }

        /// <summary>
        /// Sets the foreground and background color.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int SetColor(ILuaState state)
        {
            var foreground = state.L_CheckString(1);
            var background = "black";

            if (!state.IsNoneOrNil(2))
                background = state.L_CheckString(2);

            if (!Enum.TryParse<ConsoleColor>(foreground, true, out var consoleFg))
                return state.L_Error("invalid foreground color: '{0}'", foreground);

            if (!Enum.TryParse<ConsoleColor>(background, true, out var consoleBg))
                return state.L_Error("invalid background color: '{0}'", background);

            Console.ForegroundColor = consoleFg;
            Console.BackgroundColor = consoleBg;
            return 0;
        }

        /// <summary>
        /// Rests the foreground and background color.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int ResetColor(ILuaState state)
        {
            Console.ResetColor();
            return 0;
        }

        /// <summary>
        /// Writes a string with a new line.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int WriteLn(ILuaState state)
        {
            var text = state.IsNoneOrNil(1) ? "" : state.L_CheckString(1);

            var foreground = state.IsNoneOrNil(2) ? "white" : state.L_CheckString(2);
            var background = state.IsNoneOrNil(3) ? "black" : state.L_CheckString(3);

            if (!Enum.TryParse<ConsoleColor>(foreground, true, out var fg))
                return state.L_Error("invalid foreground color: '{0}'", foreground);

            if (!Enum.TryParse<ConsoleColor>(background, true, out var bg))
                return state.L_Error("invalid background color: '{0}'", background);

            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            Console.WriteLine(text);

            Console.ResetColor();

            return 0;
        }

        /// <summary>
        /// Writes a string without a new line.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int Write(ILuaState state)
        {
            var text = state.IsNoneOrNil(1) ? "" : state.L_CheckString(1);

            var foreground = state.IsNoneOrNil(2) ? "white" : state.L_CheckString(2);
            var background = state.IsNoneOrNil(3) ? "black" : state.L_CheckString(3);

            if (!Enum.TryParse<ConsoleColor>(foreground, true, out var fg))
                return state.L_Error("invalid foreground color: '{0}'", foreground);

            if (!Enum.TryParse<ConsoleColor>(background, true, out var bg))
                return state.L_Error("invalid background color: '{0}'", background);

            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            Console.Write(text);

            Console.ResetColor();

            return 0;
        }

        /// <summary>
        /// Reads a string from the user, use this over io.read()
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int ReadString(ILuaState state)
        {
            var prompt = string.Empty;

            if (!state.IsNoneOrNil(1))
                prompt = state.L_CheckString(1);

            Console.Write(prompt);

            var input = Console.ReadLine();
            input ??= string.Empty;

            state.PushString(input);
            return 1;
        }

        /// <summary>
        /// Gets the current working directory,
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetCwd(ILuaState state)
        {
            var cwd = Directory.GetCurrentDirectory();
            state.PushString(cwd);
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
                    return state.L_Error("invalid log level: '{0}'", level);
            }

            return 0;
        }
    }
}