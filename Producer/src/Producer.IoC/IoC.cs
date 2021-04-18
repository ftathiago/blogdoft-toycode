using Microsoft.Extensions.DependencyInjection;
using Producer.Business.Extensions;
using Producer.InfraData.Extensions;
using Producer.InfraKafka.Extensions;
using Producer.Shared.Holders;
using System.Diagnostics.CodeAnalysis;

namespace Producer.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoC
    {
        public static IServiceCollection ProjectsIocConfig(this IServiceCollection services) =>
            services
                .AddBusiness()
                .AddInfraData()
                .AddInfraKafka()
                .AddScoped<IMessageHolder, MessageHolder>();
    }
}
