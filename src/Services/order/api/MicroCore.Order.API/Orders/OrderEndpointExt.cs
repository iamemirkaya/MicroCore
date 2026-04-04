namespace MicroCore.Order.API.Orders;

public static class OrderEndpointExt
{
    public static void AddOrderGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("api/orders").WithTags("Orders")
            .CreateOrderGroupItemEndpoint()
            .GetOrdersGroupItemEndpoint().RequireAuthorization("Password");
    }
}