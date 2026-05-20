using UdemyNewMicroservice.Shared;

namespace MicroCore.Payment.Api.Feature.Create;

public record CreatePaymentCommand(
    string OrderCode,
    string CardNumber,
    string CardHolderName,
    string CardExpirationDate,
    string CardSecurityNumber,
    decimal Amount) : IRequestByServiceResult<CreatePaymentResponse>;
