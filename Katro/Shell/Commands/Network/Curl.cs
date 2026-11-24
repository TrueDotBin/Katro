using Cosmos.Kernel.System.Diagnostics;
using Katro.Constants.Shell;
using Katro.Enums;
using Katro.Network;
using Katro.Output;

namespace Katro.Shell.Commands.Network
{
    /// <summary>
    /// Gets the contents of a URL and displays them.
    /// </summary>
    public sealed class Curl : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "curl";

        /// <inheritdoc/>
        public override string Description { get; } = "Gets the contents of a URL and displays them.";

        /// <inheritdoc/>
        public override string Usage { get; } = "curl <url>";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.Network;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var url = ctx.GetArgument(0);

            if (string.IsNullOrEmpty(url))
                return CommandReturnCode.MissingArgument;

            url = UrlHelper.Normalize(url);

            using var response = HttpManager.Http.Get(url);

            if (!response.IsSuccessStatusCode)
            {
                Terminal.WriteLine($"GET request to {url} failed");
                return CommandReturnCode.GeneralFailure;
            }

            var content = response.Content.ReadAsString();
            Terminal.WriteLine(content);

            return CommandReturnCode.Success;
        }
    }
}
