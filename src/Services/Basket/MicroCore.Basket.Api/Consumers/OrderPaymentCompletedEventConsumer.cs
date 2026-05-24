using MassTransit;
using MicroCore.Basket.Api.Features.Baskets;
using MicroCore.Bus.Events;

namespace MicroCore.Basket.Api.Consumers;


public class OrderPaymentCompletedEventConsumer(BasketService basketService, ILogger<OrderPaymentCompletedEventConsumer> logger)
    : IConsumer<OrderPaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<OrderPaymentCompletedEvent> context)
    {
        logger.LogInformation("OrderPaymentCompletedEvent yakalandı. OrderCode: {OrderCode}, UserId: {UserId}",
            context.Message.OrderCode, context.Message.UserId);

        await basketService.DeleteBasket(context.Message.UserId);

        logger.LogInformation("{UserId} ID'li kullanıcının sepeti başarıyla temizlendi.", context.Message.UserId);
    }
}