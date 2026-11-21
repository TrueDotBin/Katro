using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Output.Sinks
{
    /// <summary>
    /// An output sink that writes directly to <see cref="Console"/>
    /// </summary>
    public sealed class ConsoleSink : OutputSink
    {
        /// <inheritdoc/>
        public override ConsoleColor GetForegroundColor()
            => Console.ForegroundColor;

        /// <inheritdoc/>
        public override ConsoleColor GetBackgroundColor()
            => Console.BackgroundColor;

        /// <inheritdoc/>
        public override void SetForegroundColor(ConsoleColor color)
            => Console.ForegroundColor = color;

        /// <inheritdoc/>
        public override void SetBackgroundColor(ConsoleColor color)
            => Console.BackgroundColor = color;

        /// <inheritdoc/>
        public override void WriteLine(object? value)
            => Console.WriteLine(value);

        /// <inheritdoc/>
        public override void Write(object value)
            => Console.Write(value);

        /// <inheritdoc/>
        public override void Clear()
            => Console.Clear();
    }
}
