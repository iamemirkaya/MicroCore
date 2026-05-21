using MediatR;
using MicroCore.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;


public class CheckoutBasketCommandHandler(BasketService basketService, IIdentityService identityService)
    : IRequestHandler<CheckoutBasketCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson))
        {
            var problemDetails = new ProblemDetails { Title = "Sepet bulunamadı veya boş." };
            return ServiceResult.Error(problemDetails, HttpStatusCode.NotFound);
        }

        var currentBasket = JsonSerializer.Deserialize<MicroCore.Basket.Api.Models.Basket>(basketAsJson);

        if (currentBasket == null || !currentBasket.Items.Any())
        {
            var problemDetails = new ProblemDetails { Title = "Sepetinizde ürün bulunmamaktadır." };
            return ServiceResult.Error(problemDetails, HttpStatusCode.BadRequest);
        }

        return ServiceResult.SuccessAsNoContent();
    }
}