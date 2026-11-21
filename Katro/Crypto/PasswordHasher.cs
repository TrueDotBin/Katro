using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities;
using System.Text;

namespace Katro.Crypto
{
    /// <summary>
    /// A tool that hashes passwords, or anything that is considered sensitive information.
    /// </summary>
    public static class PasswordHasher
    {
        private const int HashSize = 32;
        private const int Iterations = 100000;

        private static byte[] Sha256(byte[] data)
        {
            var hash = new Sha256Digest();
            hash.BlockUpdate(data, 0, data.Length);

            var digestSize = hash.GetDigestSize();
            var result = new byte[digestSize];
            hash.DoFinal(result, 0);

            return result;
        }

        private static byte[] AddArrays(byte[] a, byte[] b)
        {
            var data = new byte[a.Length + b.Length];

            Buffer.BlockCopy(a, 0, data, 0, a.Length);
            Buffer.BlockCopy(b, 0, data, a.Length, b.Length);

            return data;
        }

        /// <summary>
        /// Hashes the specified password using SHA256 (with PBKDF2)
        /// </summary>
        /// <param name="salt">The salt that is used to make password hashes unique.</param>
        /// <param name="password">The password that should be hashed.</param>
        public static byte[] Hash(byte[] salt, string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var data = AddArrays(salt, passwordBytes);
            var hash = Sha256(data);
            
            return hash;
        }

        /// <summary>
        /// Verifies that the provided password's hash matches with the provided hash.
        /// </summary>
        /// <param name="password">The password that should be hashed, then compared against the specified hash.</param>
        /// <param name="hash">The hash that <paramref name="password"/>'s hash should compare against.</param>
        /// <param name="salt">The salt that is used to make hashes unique.</param>
        /// <returns>Whether <paramref name="password"/>'s hash matches with <paramref name="hash"/>.</returns>
        public static bool Verify(string password, byte[] hash, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var data = AddArrays(salt, passwordBytes);
            var compareHash = Sha256(data);

            return Arrays.AreEqual(compareHash, hash);
        }
    }
}
