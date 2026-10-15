using Cosmos.Executable.Lua;
using Katro.Extensions;
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

            // filesystem functions
            RegisterCSharpFunction(state, Chdir, "katro_chdir");
            RegisterCSharpFunction(state, GetCwd, "katro_get_cwd");
            RegisterCSharpFunction(state, GetDirs, "katro_get_dirs");
            RegisterCSharpFunction(state, GetFiles, "katro_get_files");
            RegisterCSharpFunction(state, FileExists, "katro_file_exists");
            RegisterCSharpFunction(state, DirExists, "katro_dir_exists");
            RegisterCSharpFunction(state, GetFileName, "katro_get_file_name");
            RegisterCSharpFunction(state, GetFileNameWithoutExt, "katro_get_file_name_without_ext");

            // stdout functions
            RegisterCSharpFunction(state, Log, "katro_log");
            RegisterCSharpFunction(state, SetColor, "katro_set_color");
            RegisterCSharpFunction(state, ResetColor, "katro_reset_color");
            RegisterCSharpFunction(state, Write, "katro_write");
            RegisterCSharpFunction(state, WriteLn, "katro_writeln");

            // stdin functions
            RegisterCSharpFunction(state, ReadString, "katro_read_string");

            // other functions
            RegisterCSharpFunction(state, GetVersion, "katro_get_version");
        }

        /// <summary>
        /// Gets the file name of a path without the extension.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetFileNameWithoutExt(ILuaState state)
        {
            var path = state.L_CheckString(1);

            var name = Path.GetFileNameWithoutExtension(path);
            state.PushString(name);

            return 1;
        }

        /// <summary>
        /// Gets the file name of a path.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetFileName(ILuaState state)
        {
            var path = state.L_CheckString(1);

            var name = Path.GetFileName(path);
            state.PushString(name);

            return 1;
        }

        /// <summary>
        /// Determines whether a directory exists.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int DirExists(ILuaState state)
        {
            var path = state.L_CheckString(1);

            var exists = Directory.Exists(path);
            state.PushBoolean(exists);

            return 1;
        }

        /// <summary>
        /// Determines whether a file exists.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int FileExists(ILuaState state)
        {
            var path = state.L_CheckString(1);

            var exists = File.Exists(path);
            state.PushBoolean(exists);

            return 1;
        }

        /// <summary>
        /// Gets a list of all files.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetFiles(ILuaState state)
        {
            var path = Directory.GetCurrentDirectory();

            if (!state.IsNoneOrNil(1))
                path = state.L_CheckString(1);

            var files = Directory.GetFiles(path);

            state.PushStringTable(files);

            return 1;
        }

        /// <summary>
        /// Gets a list of all directories.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetDirs(ILuaState state)
        {
            var path = Directory.GetCurrentDirectory();

            if (!state.IsNoneOrNil(1))
                path = state.L_CheckString(1);

            var dirs = Directory.GetDirectories(path);

            state.PushStringTable(dirs);

            return 1;
        }

        /// <summary>
        /// Gets the version of Katro.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int GetVersion(ILuaState state)
        {
            state.PushString(SystemInfo.Version);
            return 1;
        }

        /// <summary>
        /// Changes the current directory.
        /// </summary>
        /// <param name="state">The Lua state.</param>
        public static int Chdir(ILuaState state)
        {
            var dir = state.L_CheckString(1);

            if (!Directory.Exists(dir))
            {
                state.PushBoolean(false);
            }
            else
            {
                Directory.SetCurrentDirectory(dir);
                state.PushBoolean(true);
            }

            return 1;
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