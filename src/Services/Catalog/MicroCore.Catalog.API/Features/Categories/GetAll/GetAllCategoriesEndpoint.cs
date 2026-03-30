using MediatR;

namespace MicroCore.Catalog.API.Features.Categories.GetAll;

public static class GetAllCategoriesEndpoint
{
    public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/",
                async (IMediator mediator) =>
                    (await mediator.Send(new GetAllCategoriesQuery())))
            .WithName("GetAllCategory").RequireAuthorization(policyNames: "ClientCredential");


        return group;
    }
}