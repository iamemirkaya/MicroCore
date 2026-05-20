namespace MicroCore.Payment.Api.Feature.Create;

public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);
