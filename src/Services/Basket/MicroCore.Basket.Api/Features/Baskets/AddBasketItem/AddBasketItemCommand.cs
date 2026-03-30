using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.AddBasketItem;

public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl)
    : IRequestByServiceResult;
