namespace Rtm.BlazorClient.Models;

public class CreditCardInfoModel
{
    public int Id { get; init; }
    public required string Owner { get; init; }
    public required string Number { get; init; }
    public required string Cvc { get; init; }
    public int ExpirationMonth { get; init; }
    public int ExpirationYear { get; init; }
}