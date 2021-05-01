using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.WarmUp.Services
{
    internal class PreloadingCommand : BaseWarmCommand
    {
        private const string LogMessage = "Pre-loading: {0}.";
        private const string PreloadingStart = "Preloading services.";
        private const string PreloadingEnd = "Preloading services finished.";

        public PreloadingCommand(
            IServiceCollection services,
            IServiceProvider provider,
            Action<string> logInfo,
            Action<string> logError,
            Action<string> logTrace)
            : base(services, provider, logInfo, logError, logTrace)
        {
        }

        public override Task Execute()
        {
            LogInfo(PreloadingStart);
            Task.Run(() =>
            {
                using var scope = Provider.CreateScope();
                foreach (var type in GetServices())
                {
                    scope.ServiceProvider.GetServices(type);

                    var logMessage = string.Format(LogMessage, type.FullName);
                    LogTrace(logMessage);
                }
                LogInfo(PreloadingEnd);
            });

            return Task.CompletedTask;
        }

        private IEnumerable<Type> GetServices() =>
            Services
                .Where(
                    descriptor => descriptor.ImplementationType != typeof(PreloadingCommand)
                    && !descriptor.ServiceType.ContainsGenericParameters)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();
    }
}
