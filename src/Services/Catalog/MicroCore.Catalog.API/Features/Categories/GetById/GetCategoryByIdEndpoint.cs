using MediatR;

namespace MicroCore.Catalog.API.Features.Categories.GetById;

public static class GetCategoryByIdEndpoint
{
    public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}",
                async (IMediator mediator, Guid id) =>
                    (await mediator.Send(new GetCategoryByIdQuery(id))))
            .WithName("GetByIdCategory");


        return group;
    }
}