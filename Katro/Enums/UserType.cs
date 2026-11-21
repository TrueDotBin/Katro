using Katro.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Enums
{
    /// <summary>
    /// The type of a <see cref="User"/>.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// This user can do everything.
        /// </summary>
        /// <remarks>
        /// With great power comes <b>great responsibility.</b>
        /// </remarks>
        Superuser,

        /// <summary>
        /// This user requires superuser access for sensitive actions.
        /// </summary>
        Normal
    }
}
