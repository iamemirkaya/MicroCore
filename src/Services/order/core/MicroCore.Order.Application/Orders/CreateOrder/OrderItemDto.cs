using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCore.Order.Application.Orders.CreateOrder;

public record OrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice);
