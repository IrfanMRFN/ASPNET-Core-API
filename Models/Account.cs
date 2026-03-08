namespace AspNetCoreApi.Models;

public class Account
{
    public int Id { get; set; }
    public required string AccountHolder { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = [];
}
