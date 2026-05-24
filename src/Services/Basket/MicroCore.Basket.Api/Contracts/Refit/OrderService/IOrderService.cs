using Refit;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Contracts.Refit.OrderService
{
    public interface IOrderService
    {
        [Post("/api/orders")]
        Task<ServiceResult<CreateOrderResponse>> CreateOrderAsync([Body] CreateOrderRequest request);
    }
}
