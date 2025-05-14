namespace Database.Models;

public class CreditCardInfo
{
    public int Id { get; set; }
    public required string Owner { get; set; }
    public required string Number { get; set; }
    public required string Cvc { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
}