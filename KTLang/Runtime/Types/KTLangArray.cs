using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// A list of values.
    /// </summary>
    public sealed class KTLangArray : KTLangType
    {
        private readonly List<KTLangType?> _values = [];

        /// <summary>
        /// Gets the length of this array.
        /// </summary>
        public int GetLength()
            => _values.Count;

        /// <summary>
        /// Gets the nth element of this array.
        /// </summary>
        public object? At(int index)
        {
            if (index < 0 || index > _values.Count)
                throw new IndexOutOfRangeException($"Cannot go below 0 or above {_values.Count}");

            return _values[index];
        }

        /// <summary>
        /// Adds a new element to this array.
        /// </summary>
        public void Push(KTLangType? value)
            => _values.Add(value);

        /// <summary>
        /// Removes and returns the last element from this array.
        /// </summary>
        public KTLangType? Pop()
        {
            if (_values.Count == 0)
                throw new InvalidOperationException("Cannot remove element from empty array");

            var last = _values.Last();
            _values.RemoveAt(_values.Count - 1);

            return last;
        }
    }
}
