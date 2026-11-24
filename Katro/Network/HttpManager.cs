using Cosmos.Network.Http;
using HttpClient = Cosmos.Network.Http.HttpClient;

namespace Katro.Network
{
    /// <summary>
    /// Allows the kernel to perform HTTP (and HTTPS!) requests.
    /// </summary>
    public static class HttpManager
    {
        private static bool _initialized;

        /// <summary>
        /// The current HTTP client.
        /// </summary>
        public static HttpClient Http { get; private set; }

        /// <summary>
        /// Initializes the HTTP client.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized)
                return;

            Http = new()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _initialized = true;
        }

        /// <summary>
        /// Closes the HTTP client.
        /// </summary>
        public static void Close()
        {
            if (!_initialized)
                return;

            Http.Dispose();
            _initialized = false;
        }
    }
}
