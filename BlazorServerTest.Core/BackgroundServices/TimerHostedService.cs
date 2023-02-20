using BlazorServerTest.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorServerTest.Core.BackgroundServices;
public class TimerHostedService : BackgroundService
{
    private readonly ILogger<TimerHostedService> _logger;
    private Timer? _timer = null;
    private readonly IServiceProvider _serviceProvider;

    public TimerHostedService(ILogger<TimerHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scoped service running");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5)); //TimeSpan.FromMinutes(5)
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Scoped service is working");

        using (var scope = _serviceProvider.CreateScope())
        {
            var ticketService = scope.ServiceProvider.GetRequiredService<AuxService>();
            ticketService.JobImitation();
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scoped service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        await base.StopAsync(cancellationToken);
    }
}
