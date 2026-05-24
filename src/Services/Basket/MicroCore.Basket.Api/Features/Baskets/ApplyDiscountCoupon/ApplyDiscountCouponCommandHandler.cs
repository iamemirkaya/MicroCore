using MediatR;
using MicroCore.Basket.Api.Dto;
using System.Net;
using System.Text.Json;
using UdemyNewMicroservice.Shared;
using MicroCore.Discount.Grpc;

namespace MicroCore.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandHandler(
    BasketService basketService,
    DiscountProtoService.DiscountProtoServiceClient discountProtoClient) 
    : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);
        if (string.IsNullOrEmpty(basketAsJson))
            return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);

        var basket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsJson)!;
        if (!basket.Items.Any())
            return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);

        float discountRate = 0;
        try
        {
            var grpcRequest = new GetDiscountByCodeRequest { Code = request.Coupon };
            var discountResponse = await discountProtoClient.GetDiscountByCodeAsync(grpcRequest, cancellationToken: cancellationToken);

            discountRate = discountResponse.Rate;
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            return ServiceResult.Error("Geçersiz veya süresi dolmuş kupon kodu.", HttpStatusCode.BadRequest);
        }
        basket.ApplyNewDiscount(request.Coupon, discountRate);

        basketAsJson = JsonSerializer.Serialize(basket);
        await basketService.CreateBasketCacheAsync(basket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}