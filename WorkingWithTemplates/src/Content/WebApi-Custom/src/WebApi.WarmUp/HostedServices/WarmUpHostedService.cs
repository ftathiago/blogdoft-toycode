using Microsoft.Extensions.Hosting;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using WebApi.WarmUp.Services;

namespace WebApi.WarmUp.HostedServices
{
    internal class WarmUpHostedService : BackgroundService
    {
        private readonly IImmutableList<BaseWarmCommand> _commands;

        public WarmUpHostedService(
            PreloadingCommand preloading,
            WarmUpExecutor warmUpExecutor) =>
            _commands = ImmutableList.Create<BaseWarmCommand>(
                preloading,
                warmUpExecutor);

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            foreach (var command in _commands)
            {
                await command.Execute();
            }
        }
    }
}
