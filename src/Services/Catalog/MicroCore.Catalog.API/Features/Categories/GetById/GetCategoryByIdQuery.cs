using MicroCore.Catalog.API.Features.Categories.Dtos;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Catalog.API.Features.Categories.GetById;

public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
