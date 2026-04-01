using MediatR;

namespace MicroCore.Basket.Api.Features.Baskets.GetBasket;


public static class GetBasketQueryEndpoint
{
    public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user",
                async (IMediator mediator) =>
                    (await mediator.Send(new GetBasketQuery())))
            .WithName("GetBasket");


        return group;
    }
}