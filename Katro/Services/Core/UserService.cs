using TrueDotBin.CosmosUtils.Logging;
using Katro.Services.Abstractions;
using Katro.Configuration;
using Katro.Users;

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
            Logger.Log(LogLevel.Info, $"Loading users from {UserManager.UserDatabaseFilePath}");

            try
            {
                UserManager.Load();

                if (UserManager.GetUsers().Count == 0)
                {
                    Logger.Log(LogLevel.Warning, "No users found, creating default user");
                    UserManager.AddUser("user", string.Empty);
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
            Logger.Log(LogLevel.Info, $"Saving users to {UserManager.UserDatabaseFilePath}");

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
