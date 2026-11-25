using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;
using Cosmos.Network.Http;
using HttpMethod = Cosmos.Network.Http.HttpMethod;
using HttpRequestMessage = Cosmos.Network.Http.HttpRequestMessage;
using HttpCompletionOption = Cosmos.Network.Http.HttpCompletionOption;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Sends a request to a URL and displays the response.
    /// </summary>
    public sealed class Curl : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "curl";

        /// <inheritdoc/>
        public override string Description { get; } = "Sends a request to a URL and displays the response.";

        /// <inheritdoc/>
        public override string Usage { get; } = "curl <url> [--method=GET]";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Network;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var url = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(url))
                return CommandReturnCode.MissingArgument;

            url = UrlHelper.Normalize(url);

            if (!ctx.TryGetOption("method", out var methodStr))
            {
                Terminal.WriteLine("No method provided, using GET");
                methodStr = "GET";
            }

            var method = new HttpMethod(methodStr);
            var request = new HttpRequestMessage(method, url);

            using var response = HttpManager.Http.Send(request, HttpCompletionOption.ResponseContentRead);

            if (!response.IsSuccessStatusCode)
            {
                Terminal.WriteLine($"Request to {url} failed");
                return CommandReturnCode.GeneralFailure;
            }

            var content = response.Content.ReadAsString();
            Terminal.WriteLine(content);

            return CommandReturnCode.Success;
        }
    }
}
