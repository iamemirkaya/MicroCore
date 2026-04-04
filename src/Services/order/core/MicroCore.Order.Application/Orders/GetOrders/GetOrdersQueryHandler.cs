using MediatR;
using MicroCore.Order.Application.Contracts.Repositories;
using MicroCore.Order.Application.Orders.CreateOrder;
using MicroCore.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Order.Application.Orders.GetOrders;

public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{
    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);

        var response = orders.Select(o =>
            new GetOrdersResponse(
                o.Created,
                o.TotalPrice,
                o.OrderItems.Select(item => new OrderItemDto(item.ProductId, item.ProductName, item.UnitPrice)).ToList()
            )).ToList();

        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}