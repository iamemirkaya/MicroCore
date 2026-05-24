using MediatR;
using MicroCore.Shared.Filters;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;


public static class CheckoutBasketEndpoint
{
    public static RouteGroupBuilder CheckoutBasketGroupEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/checkout",
                async (CheckoutBasketCommand command, IMediator mediator) =>
                    (await mediator.Send(command)))
            .WithName("CheckoutBasket")
            .AddEndpointFilter<ValidationFilter<CheckoutBasketCommand>>();

        return group;
    }
}