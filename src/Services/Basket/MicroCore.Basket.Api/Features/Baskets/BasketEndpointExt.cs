 using MicroCore.Basket.Api.Features.Baskets.AddBasketItem;
using MicroCore.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;
using MicroCore.Basket.Api.Features.Baskets.DeleteBasketItem;
using MicroCore.Basket.Api.Features.Baskets.GetBasket;
using MicroCore.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace MicroCore.Basket.Api.Features.Baskets;


public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/baskets")
            .AddBasketItemGroupItemEndpoint()
            .DeleteBasketItemGroupItemEndpoint()
            .GetBasketGroupItemEndpoint()
            .ApplyDiscountCouponGroupItemEndpoint()
            .CheckoutBasketGroupEndpoint()
            .RemoveDiscountCouponGroupItemEndpoint().RequireAuthorization("Password"); 

    }
}