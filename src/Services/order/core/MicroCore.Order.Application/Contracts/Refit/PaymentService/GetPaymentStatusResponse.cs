using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCore.Order.Application.Contracts.Refit.PaymentService;

public record GetPaymentStatusResponse(Guid? PaymentId, bool IsPaid);