using AspNetCoreApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
   options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
});

var app = builder.Build();

// Endpoint 1: Get all accounts
app.MapGet("/api/accounts", async (BankContext db) =>
{
    var accounts = await db.Accounts.ToListAsync();

    if (accounts.Count == 0)
    {
        // Results.NotFound() creates a standard 404 HTTP response.
        return Results.NotFound(new { Message = "No accounts found in the database."});
    }

    // Results.Ok() creates a standard 200 HTTP response.
    return Results.Ok(accounts);
});

// Endpoint 2: Get a specific account by ID (e.g., /api/account/1)
app.MapGet("/api/accounts/{id}", async (int id, BankContext db) =>
{
   // Fetch the account and its transactions using Eager Loading
   var account = await db.Accounts
        .Include(a => a.Transactions)
        .FirstOrDefaultAsync(a => a.Id == id); 

    if (account == null)
    {
        return Results.NotFound(new { Message = $"Account with ID {id} not found." });
    }

    return Results.Ok(account);
});

await app.RunAsync();