using MediatR;
using MicroCore.Catalog.API.Features.Categories.Dtos;
using System.Net;
using System;
using UdemyNewMicroservice.Shared;
using MicroCore.Catalog.API.Data;

namespace MicroCore.Catalog.API.Features.Categories.GetById;

public class GetCategoryByIdQueryHandler(CatalogDbContext context)
    : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
{
    public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var hasCategory = await context.Categories.FindAsync([request.Id], cancellationToken: cancellationToken);

        if (hasCategory == null)
            return ServiceResult<CategoryDto>.Error("Category not found",
                $"The category with id({request.Id}) was not found", HttpStatusCode.NotFound);
        var categoryAsDto = new CategoryDto(hasCategory.Id, hasCategory.Name);

        return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
    }
}