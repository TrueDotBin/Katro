using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Katro.Utils
{
    /// <summary>
    /// Utility class for loading embedded resources.
    /// </summary>
    public static class EmbeddedResourceManager
    {
        /// <summary>
        /// Gets an embedded resource via its <paramref name="path"/>
        /// </summary>
        /// <param name="path">The path of the target resource.</param>
        /// <returns>An array containing the resource data.</returns>
        public static byte[] GetEmbeddedResource(string path)
        {
            var assembly = typeof(Kernel).Assembly;

            using var stream = assembly.GetManifestResourceStream(path);
            using var ms = new MemoryStream();

            stream?.CopyTo(ms);

            return ms.ToArray();
        }
    }
}
