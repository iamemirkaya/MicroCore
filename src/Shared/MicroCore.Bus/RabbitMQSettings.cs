using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCore.Bus
{
    public static class RabbitMQSettings
    {
        public const string Order_PaymentCompletedEventQueue = "order-payment-completed-event-queue";

        public const string Order_PaymentFailedEventQueue = "order-payment-failed-event-queue";

        public const string Catalog_OrderCompletedEventQueue = "catalog-order-completed-event-queue";

        public const string Basket_OrderCompletedEventQueue = "basket-order-completed-event-queue";

        public const string Notification_OrderCompletedEventQueue = "notification-order-completed-event-queue";

        public const string Notification_OrderFailedEventQueue = "notification-order-failed-event-queue";
    }
}
