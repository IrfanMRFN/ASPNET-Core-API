var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint 1: The Root (A simple health check)
app.MapGet("/", () => "Welcome to the C# Bank API! The server is running perfectly.");

// Endpoint 2: A Custom JSON Response
app.MapGet("/api/status", () =>
{
    return new
    {
        Message = "All systems operational",
        ServerTime = DateTime.UtcNow,
        Version = "1.0.0"
    };
});

app.Run();