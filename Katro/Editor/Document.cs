using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Editor
{
    /// <summary>
    /// A document for the text editor (ktedit)
    /// </summary>
    public class Document(string path)
    {
        /// <summary>
        /// An empty document.
        /// </summary>
        public static readonly Document Empty = new(string.Empty);

        /// <summary>
        /// The file path for this document.
        /// </summary>
        public string? Path { get; set; } = path;

        /// <summary>
        /// The lines of this document.
        /// </summary>
        public List<string> Lines { get; set; } = [];

        /// <summary>
        /// Whether this document has unsaved changes.
        /// </summary>
        public bool Dirty { get; set; } = false;

        /// <summary>
        /// The row position of the cursor in this document.
        /// </summary>
        public int Line { get; set; } = 0;

        /// <summary>
        /// Inserts a line.
        /// </summary>
        /// <param name="line">The line to insert.</param>
        public void InsertLine(string line)
            => Lines.Add(line);

        /// <summary>
        /// Changes a line's content.
        /// </summary>
        /// <param name="line">The line to change the content of.</param>
        /// <param name="newContent">The new content.</param>
        public void ChangeLine(int line, string newContent)
        {
            if (line < 0 || line > Lines.Count)
                return;

            Lines[line] = newContent;
        }

        /// <summary>
        /// Saves this document to a file.
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrEmpty(Path))
                return;

            File.WriteAllLines(Path, Lines);
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        public void Load()
        {
            if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
                return;

            var lines = File.ReadAllLines(Path);
            Lines = lines.ToList();
        }
    }
}
