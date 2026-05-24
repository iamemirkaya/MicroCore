using MediatR;
using MicroCore.Order.Application.Orders.CreateOrder;
using MicroCore.Shared.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MicroCore.Order.API.Orders;


public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("",
                async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
                (await mediator.Send(command)))
            .WithName("CreateOrder")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>()
            .RequireAuthorization();

        return group;
    }
}
