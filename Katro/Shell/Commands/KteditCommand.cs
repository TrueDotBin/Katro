using Katro.Editor;
using Katro.Enums;
using Katro.FileSystem;
using Katro.Logging;
using Katro.Shell.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Shell.Commands
{
    public class KteditCommand : Command
    {
        public override string Name { get; } = "ktedit";
        public override string Description { get; } = "Katro Text Editor";
        public override string Usage { get; } = "ktedit [path]";
        public override string[] Aliases { get; } = ["kte", "kted", "edit"];
        public override CommandArgument[] Args { get; } =
        [
            new PositionalArgument("path", 0, "The path of the file to edit", false)
        ];

        public override int Run()
        {
            if (!FileSystemManager.EnsureFilesystem())
                return (int)CommandReturnCode.Success;

            var path = string.Empty;

            if (TryGetPositional(0, out var pos))
            {
                var value = pos.Value?.ToString();

                if (string.IsNullOrEmpty(value))
                    return (int)CommandReturnCode.BadArgument;

                path = value;
            }

            if (!string.IsNullOrEmpty(path) && !File.Exists(path))
                return (int)CommandReturnCode.FileNotFound;

            try
            {
                KatroTextEditor.Load(path);
                KatroTextEditor.Start();
                KatroTextEditor.Run();
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
