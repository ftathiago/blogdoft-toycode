#nullable enable
using Consumer.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.Workers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly CancellationTokenSource _cts;

        public Worker(
            ILogger<Worker> logger,
            IServiceProvider provider,
            IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _provider = provider;
            _applicationLifetime = applicationLifetime;
            _cts = new CancellationTokenSource();
            Console.CancelKeyPress += CancelKeyPress;
        }

        public override void Dispose()
        {
            Console.CancelKeyPress -= CancelKeyPress;
            base.Dispose();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopped at {0}.", DateTimeOffset.Now);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(TerminateApplication);

            var token = BuildToken();

            using var scope = _provider.CreateScope();
            var receiver = scope.ServiceProvider.GetRequiredService<IEventReceiver>();
            var handler = scope.ServiceProvider.GetRequiredService<ISaleEventHandler>();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await receiver.Execute(handler.Handle, token);
                }
                catch (OperationCanceledException)
                {
                    _cts.Cancel();
                    break;
                }
                catch (Exception exception)
                {
                    _logger.LogInformation(exception, exception.Message);
                    _cts.Cancel();
                    break;
                }
            }

            TerminateApplication();
        }

        private CancellationToken BuildToken()
        {
            var token = _cts.Token;
            token.Register(TerminateApplication);

            return token;
        }

        private void TerminateApplication()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            _applicationLifetime.StopApplication();
        }

        private void CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            _logger.LogInformation("Canceling...");
            _cts.Cancel();
            e.Cancel = true;
        }
    }
}
