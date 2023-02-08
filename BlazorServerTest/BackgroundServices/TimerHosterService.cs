﻿using BlazorServerTest.Services.Interfaces;

namespace BlazorServerTest.BackgroundServices;
public class TimerHosterService : BackgroundService
{
    private readonly ILogger<TimerHosterService> _logger;
    private Timer? _timer = null;
    private readonly IServiceProvider _serviceProvider;

    public TimerHosterService(ILogger<TimerHosterService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scoped service running");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)); //TimeSpan.FromMinutes(5)
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        _logger.LogInformation("Scoped service is working");

        using (var scope = _serviceProvider.CreateScope())
        {
            var ticketService = scope.ServiceProvider.GetRequiredService<IBackgroundService>();
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
