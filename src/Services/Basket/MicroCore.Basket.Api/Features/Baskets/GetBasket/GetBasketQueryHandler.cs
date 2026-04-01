using MediatR;
using MicroCore.Basket.Api.Dto;
using System.Net;
using System.Text.Json;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.GetBasket;


public class GetBasketQueryHandler(BasketService basketService)
    : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
{
    public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basketAsString = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsString))
            return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);

        var basket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsString)!;

        var basketItemDtos = basket.Items.Select(item => new BasketItemDto(
            item.Id,
            item.Name, 
            item.ImageUrl,
            item.Price, 
            item.PriceByApplyDiscountRate
        )).ToList();

        var basketDto = new BasketDto(basketItemDtos)
        {
            DiscountRate = basket.DiscountRate,
            Coupon = basket.Coupon
        };

        return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
    }
}