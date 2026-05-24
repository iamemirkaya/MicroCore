using MassTransit;
using MediatR;
using MicroCore.Bus.Events;
using MicroCore.Order.Application.Contracts.Refit.PaymentService;
using MicroCore.Order.Application.Contracts.Repositories;
using MicroCore.Order.Application.Contracts.UnitOfWork;
using MicroCore.Order.Domain.Entities;
using MicroCore.Shared.Services;
using System.Net;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Order.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IGenericRepository<int, Address> addressRepository,
    IIdentityService identityService,
    IUnitOfWork unitOfWork,
    IPaymentService paymentService,
    IPublishEndpoint publishEndpoint) 
    : IRequestHandler<CreateOrderCommand, ServiceResult<CreateOrderResponse>>
{
    public async Task<ServiceResult<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return ServiceResult<CreateOrderResponse>.Error("Order items not found", "Order must have at least one item", HttpStatusCode.BadRequest);

        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };

        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate, newAddress.Id);
        foreach (var orderItem in request.Items)
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
        order.Address = newAddress;

        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);

        var paymentRequest = new CreatePaymentRequest(
            OrderCode: order.Code,
            CardNumber: request.Payment.CardNumber,
            CardHolderName: request.Payment.CardHolderName,
            CardExpirationDate: request.Payment.Expiration,
            CardSecurityNumber: request.Payment.Cvc,
            Amount: order.TotalPrice
        );

        var paymentResult = await paymentService.CreatePaymentAsync(paymentRequest);

        CreatePaymentResponse? paymentResponse;
        try
        {
            paymentResponse = await paymentService.CreatePaymentAsync(paymentRequest);
        }
        catch (Refit.ApiException ex)
        {
            order.Status = OrderStatus.Cancel;
            await unitOfWork.CommitAsync(cancellationToken);
            return ServiceResult<CreateOrderResponse>.Error(
                "Ödeme servisi hatası",
                ex.Message,
                HttpStatusCode.BadRequest);
        }

        if (paymentResponse == null || !paymentResponse.Status)
        {
            order.Status = OrderStatus.Cancel;
            await unitOfWork.CommitAsync(cancellationToken);
            return ServiceResult<CreateOrderResponse>.Error(
                "Ödeme alınamadı",
                paymentResponse?.ErrorMessage ?? "Banka reddetti.",
                HttpStatusCode.BadRequest);
        }

        order.SetPaidStatus(paymentResponse.PaymentId!.Value);
        await unitOfWork.CommitAsync(cancellationToken);

        var orderCompletedEvent = new OrderPaymentCompletedEvent
        {
            OrderId = order.Id,
            UserId = identityService.UserId,
            OrderCode = order.Code,
            CourseIds = request.Items.Select(x => x.ProductId).ToList()
        };

        await publishEndpoint.Publish(orderCompletedEvent, cancellationToken);

        var response = new CreateOrderResponse(order.Id, order.Code);
        return ServiceResult<CreateOrderResponse>.SuccessAsOk(response);
    }
}