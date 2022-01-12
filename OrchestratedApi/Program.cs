using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RebusBackgroundService>();
builder.Services.AddRebusHandler<StartHandler>();
builder.Services.AddRebus(cfg => cfg
    .Transport(t => t.UsePostgreSql("host=localhost;database=rebuspoc;username=api;password=api;pooling=true;maxpoolsize=30;","messages", "inputqueue"))
    .Routing(r => r.TypeBased().Map<StartCommand>("messages"))
    .Subscriptions(s => s.StoreInMemory()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Services.UseRebus(async bus => { await bus.Subscribe<StartCommand>(); });
app.Run();
