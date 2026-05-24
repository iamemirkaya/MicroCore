using MassTransit;
using MicroCore.Bus;

namespace MicroCore.Catalog.API.Consumers;

public class OrderPaymentCompletedEventConsumerDefinition : ConsumerDefinition<OrderPaymentCompletedEventConsumer>
{
    public OrderPaymentCompletedEventConsumerDefinition()
    {
        EndpointName = RabbitMQSettings.Catalog_OrderCompletedEventQueue;
    }
}