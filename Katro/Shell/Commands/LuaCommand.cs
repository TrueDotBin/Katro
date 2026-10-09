using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Lua;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class LuaCommand : Command
    {
        public override string Name { get; } = "lua";
        public override string[] Aliases { get; } = ["ktlua"];
        public override string Description { get; } = "Executes Lua scripts.";
        public override string Usage { get; } = "lua [path]";
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the Lua script to run.", false)
        ];

        public override int Run()
        {
            if (!FileSystemManager.EnsureFilesystem())
            {
                Console.WriteLine("Using Lua shell instead");

                LuaRunner.StartRepl();
                return (int)CommandReturnCode.Success;
            }

            if (!TryGetPositional(0, out var pathArg))
            {
                Console.WriteLine("No arguments, starting Lua shell");
                LuaRunner.StartRepl();
                return (int)CommandReturnCode.Success;
            }

            var path = pathArg.Value?.ToString();

            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Invalid argument, starting Lua shell");
                LuaRunner.StartRepl();
                return (int)CommandReturnCode.Success;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found, starting Lua shell");
                LuaRunner.StartRepl();
                return (int)CommandReturnCode.Success;
            }

            try
            {
                LuaRunner.RunFile(path);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return (int)CommandReturnCode.GeneralFailure;
            }

            return (int)CommandReturnCode.Success;
        }
    }
}
