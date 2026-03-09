using AspNetCoreApi.Data;
using AspNetCoreApi.DTOs;
using AspNetCoreApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Endpoint 1: Get all accounts
app.MapGet("/api/accounts", async (BankContext db) =>
{
    var accounts = await db.Accounts.ToListAsync();

    if (accounts.Count == 0)
    {
        // Results.NotFound() creates a standard 404 HTTP response.
        return Results.NotFound(new { Message = "No accounts found in the database." });
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

    // Mapping
    var responseDto = new AccountDto
    {
        Id = account.Id,
        AccountHolder = account.AccountHolder,
        Balance = account.Balance,
        Transactions = account.Transactions.Select(tx => new TransactionDto
        {
            Id = tx.Id,
            Amount = tx.Amount,
            CreatedAt = tx.CreatedAt
        }).ToList()
    };

    return Results.Ok(responseDto);
});

// Endpoint 3: Create a new account (POST)
app.MapPost("/api/Accounts", async (CreateAccountDto newAccountRequest, BankContext db) =>
{
    // Validation
    if (string.IsNullOrWhiteSpace(newAccountRequest.AccountHolder))
    {
        return Results.BadRequest(new { Message = "Account Holder name is required."});
    }

    // Mapping request
    var newDbAccount = new Account
    {
        AccountHolder = newAccountRequest.AccountHolder,
        Balance = 0m
    };

    // Save to SQL Server
    db.Accounts.Add(newDbAccount);
    await db.SaveChangesAsync();

    // Mapping response
    var responseDto = new AccountDto
    {
        Id = newDbAccount.Id,
        AccountHolder = newDbAccount.AccountHolder,
        Balance = newDbAccount.Balance,
        Transactions = []
    };

    // Return 201 Created status, the location of the new resource and the data
    return Results.Created($"api/accounts/{newDbAccount.Id}", responseDto);
});

await app.RunAsync();