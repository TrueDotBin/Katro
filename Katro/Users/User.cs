using Katro.Constants.FileSystem;
using Katro.Converters;
using Katro.Crypto;
using Katro.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Katro.Users
{
    /// <summary>
    /// A user account known to Katro.
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// The login name for this user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The hash of the user's password.
        /// </summary>
        public byte[] Hash { get; set; } = [];

        /// <summary>
        /// The salt used when hashing the password.
        /// </summary>
        public byte[] Salt { get; set; } = [];

        /// <summary>
        /// What privileges does this user have?
        /// </summary>
        public UserType Type { get; set; } = UserType.Normal;

        /// <summary>
        /// The path of the directory where the user's files are stored.
        /// </summary>
        public string HomeDirectory { get; private set; }

        /// <summary>
        /// The paths of other additional directories.
        /// </summary>
        public Dictionary<UserFolder, string> ExtraDirs { get; private set; } = [];

        private void CreateDirs()
        {
            HomeDirectory = Path.Combine(MountPoints.RootPartition, "home", Username);
            Directory.CreateDirectory(HomeDirectory);

            var values = Enum.GetValues<UserFolder>();
            foreach (var value in values)
            {
                ExtraDirs[value] = Path.Combine(HomeDirectory, value.ToString());
            }

            foreach (var dir in ExtraDirs.Values)
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// Creates a new <see cref="User"/> object.
        /// </summary>
        /// <param name="username">The login name for this user.</param>
        /// <param name="password">The password used for authentication.</param>
        /// <param name="type">What privileges this user has.</param>
        public User(string username, string password, UserType type = UserType.Normal)
        {
            Username = username;
            Salt = GenerateSalt();
            Hash = PasswordHasher.Hash(Salt, password);
            Type = type;

            CreateDirs();
        }

        internal User(string username, byte[] hash, byte[] salt, UserType type = UserType.Normal)
        {
            Username = username;
            Salt = salt;
            Hash = hash;
            Type = type;

            CreateDirs();
        }

        /// <summary>
        /// Returns a stringified entry for a user database.
        /// </summary>
        public override string ToString()
        {
            var saltBase64 = Base64.Encode(Salt);
            var hashBase64 = Base64.Encode(Hash);
            var typeInteger = (int)Type;

            return $"{Username}:{saltBase64}:{hashBase64}:{typeInteger}";
        }

        /// <summary>
        /// Parses a user entry string into a user.
        /// </summary>
        public static User? FromString(string value)
        {
            var parts = value.Split(':');

            if (parts.Length < 4)
                return null;

            var name = parts[0].Trim();

            var saltBase64 = parts[1].Trim();
            var hashBase64 = parts[2].Trim();

            var salt = Base64.DecodeBytes(saltBase64);
            var hash = Base64.DecodeBytes(hashBase64);

            var typeStr = parts[3].Trim();

            if (!int.TryParse(typeStr, out var typeInt))
                return null;

            var userType = (UserType)typeInt;
            return new User(name, hash, salt, userType);
        }

        private byte[] GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            RandomNumberGenerator.Fill(salt);
            return salt;
        }
    }
}
