namespace MicroCore.Payment.Api.Models;

public class Payment
{

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OrderCode { get; set; }
    public DateTime Created { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }

}

