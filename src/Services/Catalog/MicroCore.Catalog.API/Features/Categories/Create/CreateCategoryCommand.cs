using UdemyNewMicroservice.Shared;

namespace MicroCore.Catalog.API.Features.Categories.Create;

public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryResponse>;