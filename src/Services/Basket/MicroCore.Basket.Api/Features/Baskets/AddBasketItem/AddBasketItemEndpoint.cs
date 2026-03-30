using MediatR;
using MicroCore.Shared.Filters;

namespace MicroCore.Basket.Api.Features.Baskets.AddBasketItem;


public static class AddBasketItemEndpoint
{
    public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/item",
                async (AddBasketItemCommand command, IMediator mediator) =>
                    (await mediator.Send(command)))
            .WithName("AddBasketItem")
            .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();


        return group;
    }
}