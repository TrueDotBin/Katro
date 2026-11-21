using Katro.Enums;
using System;
using System.Collections.Generic;
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
        /// <param name="path">The path of the database file.</param>
        public static void Save(string path = "/katro/etc/users")
        {
            var sb = new StringBuilder();

            foreach (var user in _users)
            {
                var entry = user.ToString();
                sb.AppendLine(entry);
            }

            File.WriteAllText(path, sb.ToString());
        }

        /// <summary>
        /// Loads each user from a file.
        /// </summary>
        /// <param name="path">The path of the database file.</param>
        public static void Load(string path = "/katro/etc/users")
        {
            if (!File.Exists(path))
                return;

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var user = User.FromString(line);

                if (user is null)
                    continue;

                _users.Add(user);
            }
        }
    }
}
