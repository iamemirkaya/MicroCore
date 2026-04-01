using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;
