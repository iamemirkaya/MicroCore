using MediatR;
using MicroCore.Payment.Api.Data;
using MicroCore.Payment.Api.Models;
using MicroCore.Shared.Services;
using System.Net;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Payment.Api.Feature.Create;
public class CreatePaymentCommandHandler(
    PaymentDbContext appDbContext,
    IIdentityService identityService,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
{
    public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var userId = identityService.UserId;
        var userName = identityService.UserName;
        var roles = identityService.Roles;

        var newPayment = new Models.Payment
        {
            Id = Guid.NewGuid(),
            UserId = userId, 
            OrderCode = request.OrderCode,
            Amount = request.Amount,
            Created = DateTime.UtcNow,
            Status = PaymentStatus.Success
        };

        appDbContext.Payments.Add(newPayment);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreatePaymentResponse>.SuccessAsOk(
            new CreatePaymentResponse(newPayment.Id, true, null));
    }
}