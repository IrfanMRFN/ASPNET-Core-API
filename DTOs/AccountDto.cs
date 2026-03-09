namespace AspNetCoreApi.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string AccountHolder { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public List<TransactionDto> Transactions { get; set; } = [];
}

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAccountDto
{
    public required string AccountHolder { get; set; }
}
