using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;

public record CheckoutBasketCommand(
    string FirstName,
    string LastName,
    string Address,
    string City,
    string CardNumber,
    string CardHolderName,
    string Expiration,
    string CVV) : IRequestByServiceResult;