using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public KTLangNumber GetLength()
            => new(_values.Count);

        /// <summary>
        /// Gets the nth element of this array.
        /// </summary>
        public KTLangType? At(int index)
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

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => ReferenceEquals(this, obj);

        /// <inheritdoc/>
        public override int GetHashCode()
            => RuntimeHelpers.GetHashCode(this);

        /// <inheritdoc/>
        public override string ToString()
            => string.Join(", ", _values);

        /// <inheritdoc/>
        public override KTLangType? GetMember(string member)
        {
            return member switch
            {
                "length" => GetLength(),
                _ => throw new Exception($"Invalid member name '{member}'")
            };
        }

        /// <inheritdoc/>
        public override KTLangType? CallMethod(string method, List<KTLangType?> args)
        {
            switch (method)
            {
                case "pop":
                    return Pop();

                case "push":
                    {
                        if (args.Count == 0)
                            throw new Exception($"array.push() requires at least one argument");

                        var first = args.First();
                        Push(first);

                        return null;
                    }

                case "at":
                    {
                        if (args.Count == 0)
                            throw new Exception($"array.at() requires at least one argument");

                        var first = args.First();

                        if (first is KTLangNumber num)
                            return At((int)num.Value);

                        throw new Exception("Cannot index using non-number type");
                    }

                default:
                    throw new Exception($"Invalid method name '{method}'");
            }
        }
    }
}
