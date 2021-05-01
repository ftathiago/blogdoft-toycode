using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApi.WarmUp.Services
{
    internal abstract class BaseWarmCommand
    {
        protected BaseWarmCommand(
            IServiceCollection services,
            IServiceProvider provider,
            Action<string> logInfo,
            Action<string> logError,
            Action<string> logTrace)
        {
            Services = services;
            Provider = provider;
            LogInfo = logInfo;
            LogError = logError;
            LogTrace = logTrace;
        }

        protected IServiceCollection Services { get; }
        protected IServiceProvider Provider { get; }
        protected Action<string> LogInfo { get; }
        protected Action<string> LogError { get; }
        protected Action<string> LogTrace { get; }

        public abstract Task Execute();
    }
}
