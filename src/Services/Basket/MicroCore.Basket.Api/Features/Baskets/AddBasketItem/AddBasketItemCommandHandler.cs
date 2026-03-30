using MediatR;
using MicroCore.Basket.Api.Models;
using System.Text.Json;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.AddBasketItem;


public class AddBasketItemCommandHandler(BasketService basketService): IRequestHandler<AddBasketItemCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);


        MicroCore.Basket.Api.Models.Basket? currentBasket;

        var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl,
            request.CoursePrice, null);


        if (string.IsNullOrEmpty(basketAsJson))
        {
            var tempUserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            currentBasket = new MicroCore.Basket.Api.Models.Basket(tempUserId, [newBasketItem]);//_identityService.UserId
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