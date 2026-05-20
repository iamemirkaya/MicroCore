using MediatR;
using Microsoft.AspNetCore.Mvc;
using MicroCore.Shared.Extensions;

namespace MicroCore.Payment.Api.Feature.Create;

public static class CreatePaymentCommandEndpoint
{
    public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("",
                async ([FromBody] CreatePaymentCommand createPaymentCommand, IMediator mediator) =>
                (await mediator.Send(createPaymentCommand)).ToGenericResult())
            .WithName("create")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).RequireAuthorization("Password");

        return group;
    }
}