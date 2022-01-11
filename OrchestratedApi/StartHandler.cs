using Rebus.Handlers;

internal class StartHandler : IHandleMessages<StartCommand>
{
    private readonly ILogger<StartHandler> logger;

    public StartHandler(ILogger<StartHandler> logger)
    {
        this.logger = logger;
    }
    public async Task Handle(StartCommand message)
    {
        logger.LogInformation("StartHandler handling {message.Id}", message.Id);
        Console.WriteLine($"I'm handling {message.Id}");
        await Task.Yield();
    }
}