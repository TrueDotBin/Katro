using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Network
{
    /// <summary>
    /// Helpers for formatting URLs.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Prepends the https:// prefix to <paramref name="url"/>
        /// </summary>
        public static string Normalize(string url)
        {
            if (url.StartsWith("https://"))
                return url;

            return $"https://{url}";
        }
    }
}
