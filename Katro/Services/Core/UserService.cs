using Katro.Configuration;
using Katro.FileSystem;
using Katro.Services.Abstractions;
using Katro.Users;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.Services.Core
{
    /// <summary>
    /// Loads and saves the list of users.
    /// </summary>
    public class UserService : KernelService
    {
        /// <inheritdoc/>
        public override string Name { get; } = "Users";

        /// <inheritdoc/>
        public override string Id { get; } = "users";

        /// <inheritdoc/>
        public override string[] DependsOn { get; } = ["fs"];

        /// <inheritdoc/>
        public override bool Start()
        {
            Logger.Log(LogLevel.Info, $"Loading users from {Paths.UserDatabaseFilePath}");

            try
            {
                UserManager.Load();

                if (UserManager.GetUsers().Count == 0)
                {
                    Logger.Log(LogLevel.Warning, "No users found, creating default users");
                    UserManager.AddUser("root", string.Empty, Enums.UserType.Superuser);

                    Logger.Log(LogLevel.Warning, "The root account has no password, CHANGE IT IMMEDIATELY!");
                    Logger.Log(LogLevel.Warning, "passwd root");
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Failed to load users: {ex.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            Logger.Log(LogLevel.Info, $"Saving users to {Paths.UserDatabaseFilePath}");

            try
            {
                UserManager.Save();
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"Failed to save users: {ex.Message}");
            }
        }
    }
}
