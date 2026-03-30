

using MicroCore.Catalog.API.Features.Categories.Dtos;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Catalog.API.Features.Categories.GetAll;

public class GetAllCategoriesQuery : IRequestByServiceResult<List<CategoryDto>>;
