using MicroCore.Basket.Api.Features.Baskets.AddBasketItem;

namespace MicroCore.Basket.Api.Features.Baskets;


public static class BasketEndpointExt
{
    public static void AddBasketGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/baskets")
            .AddBasketItemGroupItemEndpoint();
    }
}