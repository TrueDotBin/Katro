using Katro.Constants.FileSystem;
using Katro.Crypto;
using Katro.Enums;
using Katro.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Katro.Users
{
    /// <summary>
    /// Manages all users.
    /// </summary>
    public static class UserManager
    {
        private const string InvalidUsernameChars = "/\\:*?\"<>|\0";
        private static List<User> _users = [];

        /// <summary>
        /// Whether the current session is elevated.
        /// </summary>
        public static bool Elevated { get; set; }

        /// <summary>
        /// Whether the user has seen the sudo lecture.
        /// </summary>
        public static bool SeenSudoLecture { get; set; }

        /// <summary>
        /// The path of the file that holds all users.
        /// </summary>
        public static readonly string UserDatabaseFilePath =
            Path.Combine(MountPoints.RootPartition, "etc", "users");

        /// <summary>
        /// The currently logged in user.
        /// </summary>
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        public static IReadOnlyList<User> GetUsers()
            => _users.AsReadOnly();

        /// <summary>
        /// Determines whether the provided username is valid.
        /// </summary>
        public static bool IsValidUsername(string username)
            => !username.Contains(InvalidUsernameChars);

        /// <summary>
        /// Adds a new user to the list.
        /// </summary>
        /// <param name="username">The login name for this user.</param>
        /// <param name="password">The password for this user.</param>
        /// <param name="type">What privileges this user has.</param>
        /// <exception cref="ArgumentException">Thrown when the provided username is invalid.</exception>
        public static void AddUser(string username, string password, UserType type = UserType.Normal)
        {
            if (!IsValidUsername(username))
                throw new ArgumentException($"Provided username ({username}) is invalid");

            var user = new User(username, password, type);
            _users.Add(user);
        }

        /// <summary>
        /// Determines whether the provided user has the provided type.
        /// </summary>
        public static bool IsType(User user, UserType type)
            => user.Type == type;

        /// <summary>
        /// Determines whether the currently logged in user has the provided type.
        /// </summary>
        public static bool IsType(UserType type)
            => IsType(CurrentUser, type);

        /// <summary>
        /// Saves each user to a file.
        /// </summary>
        public static void Save()
        {
            var sb = new StringBuilder();

            foreach (var user in _users)
            {
                var entry = user.ToString();
                sb.AppendLine(entry);
            }

            File.WriteAllText(UserDatabaseFilePath, sb.ToString());
        }

        /// <summary>
        /// Loads each user from a file.
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(UserDatabaseFilePath))
                return;

            var lines = File.ReadAllLines(UserDatabaseFilePath);

            foreach (var line in lines)
            {
                var user = User.FromString(line);

                if (user is null)
                    continue;

                _users.Add(user);
            }
        }

        /// <summary>
        /// Attempts to log in as a user.
        /// </summary>
        /// <param name="username">The login name of the target user.</param>
        /// <param name="password">The password of the target user.</param>
        /// <returns>An error message, or <see cref="string.Empty"/> if successful.</returns>
        public static string Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return $"User \"{username}\" not found";

            if (!PasswordHasher.Verify(password, user.Hash, user.Salt))
                return $"Invalid password";

            CurrentUser = user;
            return string.Empty;
        }

        /// <summary>
        /// Shows the sudo lecture to the user.
        /// </summary>
        public static void ShowLecture()
        {
            if (SeenSudoLecture)
                return;

            // linux moment :D
            Terminal.WriteLine("We trust you have received the usual lecture from the local System");
            Terminal.WriteLine("Administrator. It usually boils down to these three things:");
            Terminal.WriteLine();
            Terminal.WriteLine("    #1) Respect the privacy of others.");
            Terminal.WriteLine("    #2) Think before you type.");
            Terminal.WriteLine("    #3) With great power comes great responsibility.");
            Terminal.WriteLine();

            SeenSudoLecture = true;
        }
    }
}
