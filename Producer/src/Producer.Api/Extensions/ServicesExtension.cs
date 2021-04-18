using Microsoft.Extensions.DependencyInjection;
using Producer.Api.Lib;
using Producer.Api.Middlerwares;
using Producer.IoC;
using Producer.Shared.Holders;
using Producer.WarmUp.Extensions;
using Serilog;

namespace Producer.Api.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection ConfigureIoc(this IServiceCollection services) =>
            services
                .ConfigSwagger()
                .AddEndpoints()
                .ConfigureApiVersioning()
                .AddExternalDependencies()
                .ConfigureApiVersioning()
                .AddScoped<IHttpContextAccessor, HttpContextAccessor>();

        private static IServiceCollection AddEndpoints(this IServiceCollection services) =>
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<HttpContextHolderFilter>();
                    options.Filters.Add<ControllerExceptionFilter>();
                    options.Filters.Add<MessageFilter>();
                }).Services
                .AddHealthChecks().Services;

        private static IServiceCollection AddExternalDependencies(this IServiceCollection services) =>
            services
                .AddWarmUp(
                    logInfo => Log.Information(logInfo),
                    logError => Log.Error(logError),
                    logTrace => Log.Verbose(logTrace))
                .ProjectsIocConfig();
    }
}
