



using MicroCore.Catalog.API.Features.Categories.Create;
using MicroCore.Catalog.API.Features.Categories.GetAll;
using MicroCore.Catalog.API.Features.Categories.GetById;

namespace MicroCore.Catalog.API.Features.Categories;


public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/categories")
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint()
            .GetByIdCategoryGroupItemEndpoint();
    }
}