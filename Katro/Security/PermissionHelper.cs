using Katro.Enums;
using Katro.FileSystem;
using Katro.Shell.Commands;
using Katro.Users;

namespace Katro.Security
{
    /// <summary>
    /// Helpers for managing permissions.
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Checks if the current user has permission to perform an operation on the given path.
        /// </summary>
        public static bool CanAccessPath(string path)
        {
            var user = UserManager.CurrentUser;

            if (user == null)
                return false;

            if (user.Type == UserType.Superuser || UserManager.Elevated)
                return true;

            var normalizedPath = Path.GetFullPath(path);
            return PathHelper.IsInsideUserPath(normalizedPath);
        }
    }
}