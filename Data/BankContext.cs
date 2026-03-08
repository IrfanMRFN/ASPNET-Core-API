using AspNetCoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
