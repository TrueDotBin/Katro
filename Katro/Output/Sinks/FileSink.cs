using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Output.Sinks
{
    /// <summary>
    /// An output sink that writes directly to a file.
    /// </summary>
    /// <param name="path">The path of the file that this sink should write to.</param>
    /// <param name="append">Whether the file should be appended to rather than overwritten.</param>
    public sealed class FileSink(string path, bool append = false) : OutputSink
    {
        /// <summary>
        /// The path of the file that this sink writes to.
        /// </summary>
        public string FilePath { get; init; } = path;

        /// <summary>
        /// Whether the file should be appended to rather than overwritten.
        /// </summary>
        public bool Append { get; init; } = append;

        private readonly StringBuilder _sb = new();
        private readonly object _fileLock = new();

        /// <inheritdoc/>
        public override void Write(object value)
            => _sb.Append(value);

        /// <inheritdoc/>
        public override void WriteLine(object? value)
            => _sb.AppendLine(value?.ToString() ?? string.Empty);

        /// <inheritdoc/>
        public override void Clear()
            => _sb.Clear();

        /// <inheritdoc/>
        public override void OnDispose()
        {
            lock (_fileLock)
            {
                if (Append)
                    File.AppendAllText(FilePath, _sb.ToString());
                else
                    File.WriteAllText(FilePath, _sb.ToString());
            }
        }
    }
}
