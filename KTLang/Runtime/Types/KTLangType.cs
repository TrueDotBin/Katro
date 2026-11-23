using System;
using System.Collections.Generic;
using System.Text;

namespace KTLang.Runtime.Types
{
    /// <summary>
    /// Base class for all KTLang types.
    /// </summary>
    public abstract class KTLangType
    {
        /// <summary>
        /// Gets the value of a member.
        /// </summary>
        public virtual KTLangType? GetMember(string member)
            => throw new Exception("This type has no members");

        /// <summary>
        /// Calls a method on this type.
        /// </summary>
        public virtual KTLangType? CallMethod(string method, List<KTLangType?> args)
            => throw new Exception("This type has no methods");
    }
}
