using Rebus.Bus;

class RebusBackgroundService : IHostedService
{
    readonly BusLifetimeEvents _busLifetimeEvents;
    readonly ILogger<RebusBackgroundService> _logger;

    bool _disposed;

    public RebusBackgroundService(BusLifetimeEvents busLifetimeEvents, ILogger<RebusBackgroundService> logger)
    {
        _busLifetimeEvents = busLifetimeEvents ?? throw new ArgumentNullException(nameof(busLifetimeEvents));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _busLifetimeEvents.BusDisposed += () => _disposed = true;
        _logger.LogInformation("BackgroundServiceExample is started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        var busDisposed = _disposed;
        _logger.LogInformation($"BackgroundServiceExample is stopped - bus disposed={busDisposed}");
        return Task.CompletedTask;
    }
}
