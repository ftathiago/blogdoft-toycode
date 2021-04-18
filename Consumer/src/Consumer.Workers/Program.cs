using Consumer.Workers.Extensions;
using Microsoft.Extensions.Hosting;

namespace Consumer.Workers
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => services.AddIoC());
    }
}
