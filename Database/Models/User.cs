namespace Database.Models;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public int CreditCardId { get; set; }
    public CreditCardInfo? CreditCard { get; set; }
}