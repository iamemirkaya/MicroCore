using MediatR;
using MicroCore.Basket.Api.Models;
using MicroCore.Shared.Services;
using System.Text.Json;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.AddBasketItem;


public class AddBasketItemCommandHandler(BasketService basketService, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);


        MicroCore.Basket.Api.Models.Basket? currentBasket;

        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
            request.CoursePrice, null);


        if (string.IsNullOrEmpty(basketAsJson))
        {
            currentBasket = new MicroCore.Basket.Api.Models.Basket(identityService.UserId, [newBasketItem]);
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }

        currentBasket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsJson);


        var existingBasketItem = currentBasket!.Items.FirstOrDefault(x => x.Id == request.CourseId);


        if (existingBasketItem is not null)
            currentBasket.Items.Remove(existingBasketItem);

        currentBasket.Items.Add(newBasketItem);


        currentBasket.ApplyAvailableDiscount();


        await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}