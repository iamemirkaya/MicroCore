using MediatR;
using MicroCore.Basket.Api.Contracts.Refit.OrderService;
using MicroCore.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UdemyNewMicroservice.Shared;
namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;

public class CheckoutBasketCommandHandler(
    BasketService basketService,
    IOrderService orderService) 
    : IRequestHandler<CheckoutBasketCommand, ServiceResult<CreateOrderResponse>>
{
    public async Task<ServiceResult<CreateOrderResponse>> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            var problemDetails = new ProblemDetails { Title = "Sepet bulunamadı veya boş." };
            return ServiceResult<CreateOrderResponse>.Error(problemDetails, HttpStatusCode.NotFound);
        }

        var currentBasket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsJson);

        if (currentBasket == null || !currentBasket.Items.Any())
        {
            var problemDetails = new ProblemDetails { Title = "Sepetinizde ürün bulunmamaktadır." };
            return ServiceResult<CreateOrderResponse>.Error(problemDetails, HttpStatusCode.BadRequest);
        }

        var orderItems = currentBasket.Items.Select(x => new OrderItemDto(
            ProductId: x.Id,
            ProductName: x.Name,
            UnitPrice: x.Price)).ToList();

        var addressDto = new AddressDto(request.Province, request.District, request.Street, request.ZipCode, request.Line);

        var totalAmount = currentBasket.TotalPriceWithAppliedDiscount ?? currentBasket.TotalPrice;
        var paymentDto = new PaymentDto(request.CardNumber, request.CardHolderName, request.Expiration, request.CVV, totalAmount);

        var createOrderRequest = new CreateOrderRequest(
            DiscountRate: currentBasket.DiscountRate,
            Address: addressDto,
            Payment: paymentDto,
            Items: orderItems
        );

        var orderResult = await orderService.CreateOrderAsync(createOrderRequest);

        if (!orderResult.IsSuccess)
        {
            return ServiceResult<CreateOrderResponse>.Error(orderResult.Fail!, orderResult.Status);
        }

        return ServiceResult<CreateOrderResponse>.SuccessAsOk(orderResult.Data!);
    }
}