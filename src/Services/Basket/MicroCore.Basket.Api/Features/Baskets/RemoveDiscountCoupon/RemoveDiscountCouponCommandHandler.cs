using MediatR;
using MicroCore.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

public class RemoveDiscountCouponCommandHandler(IIdentityService identityService,IDistributedCache distributedCache,
    BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request,
        CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson)) return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);

        var basket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsJson);

        basket!.ClearDiscount();


        basketAsJson = JsonSerializer.Serialize(basket);

        await basketService.CreateBasketCacheAsync(basket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}