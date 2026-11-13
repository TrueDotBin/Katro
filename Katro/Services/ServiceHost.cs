using Katro.Services.Abstractions;
using TrueDotBin.CosmosUtils.Logging;

namespace Katro.Services
{
    /// <summary>
    /// Manages all the kernel's services.
    /// </summary>
    public static class ServiceHost
    {
        private static readonly List<IKernelService> _services = [];
        private static readonly Logger _logger = new("Service Host");

        /// <summary>
        /// Starts a service, checking if its dependencies have been resolved first.
        /// </summary>
        /// <param name="service">The service that should be started.</param>
        public static void Start(IKernelService service)
        {
            if (_services.Contains(service))
            {
                _logger.Log(LogLevel.Warning, $"\"{service.Name}\" is already running!");
                return;
            }

            var started = _services.Select(s => s.Id);

            _logger.Log(LogLevel.Info, $"Starting service \"{service.Name}\"");

            foreach (var dep in service.DependsOn)
            {
                if (!started.Contains(dep))
                {
                    _logger.Log(LogLevel.Error, $"Cannot start service \"{service.Id}\" because it depends on \"{dep}\"!");
                    return;
                }
            }

            _services.Add(service);
        }
    }
}
