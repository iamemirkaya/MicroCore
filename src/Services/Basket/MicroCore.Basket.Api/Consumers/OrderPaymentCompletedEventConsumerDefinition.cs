using MassTransit;
using MicroCore.Bus;

namespace MicroCore.Basket.Api.Consumers;

public class OrderPaymentCompletedEventConsumerDefinition : ConsumerDefinition<OrderPaymentCompletedEventConsumer>
{
    public OrderPaymentCompletedEventConsumerDefinition()
    {
        EndpointName = RabbitMQSettings.Basket_OrderCompletedEventQueue;
    }
}