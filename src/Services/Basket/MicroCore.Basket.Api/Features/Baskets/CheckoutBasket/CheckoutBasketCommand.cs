using MicroCore.Basket.Api.Contracts.Refit.OrderService;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;

public record CheckoutBasketCommand(
    string Province,
    string District,
    string Street,
    string ZipCode,
    string Line,
    string CardNumber,
    string CardHolderName,
    string Expiration,
    string CVV) : IRequestByServiceResult<CreateOrderResponse>;
