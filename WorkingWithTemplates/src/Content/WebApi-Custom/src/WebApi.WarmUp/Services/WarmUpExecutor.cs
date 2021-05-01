using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.WarmUp.Abstractions;
using WebApi.WarmUp.HealthChecks;

namespace WebApi.WarmUp.Services
{
    internal class WarmUpExecutor : BaseWarmCommand
    {
        private const string LogMessage = "Warming-up {0}.";
        private const string LogErrorMessage = "Erro when warming-up {0}: {1}";
        private readonly WarmUpHealthCheck _warmUpHealthCheck;

        public WarmUpExecutor(
            IServiceCollection services,
            IServiceProvider provider,
            Action<string> logInfo,
            Action<string> logError,
            Action<string> logTrace,
            WarmUpHealthCheck warmUpHealthCheck)
            : base(services, provider, logInfo, logError, logTrace)
        {
            _warmUpHealthCheck = warmUpHealthCheck;
        }

        public override Task Execute()
        {
            LogInfo("Executing warmup.");
            Task.Run(async () =>
            {
                using var scope = Provider.CreateScope();
                foreach (var type in GetCommands())
                {
                    var warmUpCommand = scope.ServiceProvider.GetService(type) as IWarmUpCommand;
                    await ExecuteWarmingUp(warmUpCommand);
                    var logMessage = string.Format(LogMessage, warmUpCommand.GetType().FullName);
                    LogTrace(logMessage);
                }
                _warmUpHealthCheck.WarmUpCompleted = true;
                LogInfo("Warmup finished.");
            });

            return Task.CompletedTask;
        }

        private IEnumerable<Type> GetCommands() =>
            Services
                .Where(
                    descriptor => descriptor.ImplementationType?
                        .GetInterfaces()
                        .Contains(typeof(IWarmUpCommand)) == true)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();

        private async Task ExecuteWarmingUp(IWarmUpCommand command)
        {
            try
            {
                if (command is null)
                {
                    return;
                }

                await command.Execute();
            }
            catch (Exception exception)
            {
                LogError(string.Format(LogErrorMessage, command.GetType().FullName, exception.Message));
            }
        }
    }
}
