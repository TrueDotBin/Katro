using KTLang.Runtime.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Exceptions
{
	/// <summary>
	/// Thrown whenever a function wants to return a value.
	/// </summary>
	[Serializable]
	public class ReturnException(KTLangType? value) : Exception
	{
		/// <summary>
		/// The value that should be returned.
		/// </summary>
        public KTLangType? Value { get; } = value;
    }
}
