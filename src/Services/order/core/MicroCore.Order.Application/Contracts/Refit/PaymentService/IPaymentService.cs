using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyNewMicroservice.Shared;

namespace MicroCore.Order.Application.Contracts.Refit.PaymentService;
public interface IPaymentService
{
    [Post("/api/payments/create")]
    Task<CreatePaymentResponse> CreatePaymentAsync([Body] CreatePaymentRequest request);
}