using MediatR;
using MicroCore.Catalog.API.Data;
using MicroCore.Catalog.API.Features.Categories.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Catalog.API.Features.Categories.GetAll;


public class GetAllCategoryQueryHandler(CatalogDbContext context)
    : IRequestHandler<GetAllCategoriesQuery, ServiceResult<List<CategoryDto>>>
{
    public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categoriesAsDto = await context.Categories
            .Select(category => new CategoryDto(category.Id, category.Name))
            .ToListAsync(cancellationToken);

        return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
    }
}