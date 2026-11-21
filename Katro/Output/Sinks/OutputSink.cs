using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Output.Sinks
{
    /// <summary>
    /// An output sink that can support colored output.
    /// </summary>
    public abstract class OutputSink : IDisposable
    {
        /// <summary>
        /// The output's foreground color.
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get => GetForegroundColor();
            set => SetForegroundColor(value);
        }

        /// <summary>
        /// The output's background color.
        /// </summary>
        public ConsoleColor BackgroundColor
        {
            get => GetBackgroundColor();
            set => SetBackgroundColor(value);
        }

        /// <summary>
        /// Gets the output's foreground color.
        /// </summary>
        public virtual ConsoleColor GetForegroundColor() => ConsoleColor.White;

        /// <summary>
        /// Gets the output's background color.
        /// </summary>
        public virtual ConsoleColor GetBackgroundColor() => ConsoleColor.Black;

        /// <summary>
        /// Sets the output's foreground color.
        /// </summary>
        /// <param name="color">The foreground color.</param>
        public virtual void SetForegroundColor(ConsoleColor color) { }

        /// <summary>
        /// Sets the output's background color.
        /// </summary>
        /// <param name="color">The background color.</param>
        public virtual void SetBackgroundColor(ConsoleColor color) { }

        /// <summary>
        /// Writes the specified object's string representation to the output, with a trailing line break.
        /// </summary>
        /// <param name="value">The object that should be written.</param>
        public virtual void WriteLine(object? value = null) { }

        /// <summary>
        /// Writes the specified object's string representation to the output.
        /// </summary>
        /// <param name="value">The object that should be written.</param>
        public virtual void Write(object value) { }

        /// <summary>
        /// Clears the current output.
        /// </summary>
        public virtual void Clear() { }

        /// <summary>
        /// Called whenever <see cref="Dispose()"/> is called on this method.
        /// </summary>
        public virtual void OnDispose() { }

        /// <inheritdoc/>
        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }
    }
}
