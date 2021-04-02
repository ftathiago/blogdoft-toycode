using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WebApi.Business.Extensions;
using WebApi.InfraData.Extensions;
using WebApi.Shared.Holders;

namespace WebApi.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoC
    {
        public static IServiceCollection ProjectsIocConfig(this IServiceCollection services) =>
            services
                .AddBusiness()
                .AddInfraData()
                .AddScoped<IMessageHolder, MessageHolder>();
    }
}