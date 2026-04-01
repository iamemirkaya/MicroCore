using MicroCore.Basket.Api.Dto;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.GetBasket;

public record GetBasketQuery : IRequestByServiceResult<BasketDto>;
