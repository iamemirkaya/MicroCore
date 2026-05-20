using MediatR;
using MicroCore.Order.Application.Orders.GetOrders;
using Microsoft.AspNetCore.Mvc;
namespace MicroCore.Order.API.Orders;

public static class GetOrdersEndpoint
{
    public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetOrdersQuery())))
            .WithName("GetOrders")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization();


        return group;
    }
}