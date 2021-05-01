using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using WebApi.Api.Filters;
using WebApi.IoC;
using WebApi.WarmUp.Extensions;

namespace WebApi.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddApiIoc(this IServiceCollection services) =>
            services
                .ConfigSwagger()
                .AddEndpoints()
                .ConfigureApiVersioning()
                .AddExternalDependencies();

        public static IServiceCollection AddEndpoints(this IServiceCollection services) =>
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ControllerExceptionFilter>();
                    options.Filters.Add<MessageFilter>();
                }).Services
                .AddHealthChecks().Services;

        public static IServiceCollection AddExternalDependencies(this IServiceCollection services) =>
            services
#if (!excludeWarmup)
                .AddWarmUp(
                    logInfo => Log.Information(logInfo),
                    logError => Log.Error(logError),
                    logTrace => Log.Verbose(logTrace))
#endif
                .ProjectsIocConfig();
    }
}
