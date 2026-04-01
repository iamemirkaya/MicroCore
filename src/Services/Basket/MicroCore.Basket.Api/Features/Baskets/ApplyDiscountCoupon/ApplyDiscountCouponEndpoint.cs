using MediatR;
using MicroCore.Shared.Filters;

namespace MicroCore.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public static class ApplyDiscountCouponEndpoint
{
    public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/apply-discount-coupon",
                async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                    (await mediator.Send(command)))
            .WithName("ApplyDiscountCoupon")
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
        return group;
    }
}