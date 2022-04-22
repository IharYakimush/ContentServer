using ContentServer.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddContentServer();

var app = builder.Build();

app.UseRouting();
app.UseContentServer();

app.Run();
