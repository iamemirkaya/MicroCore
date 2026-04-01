using MediatR;

namespace MicroCore.Basket.Api.Features.Baskets.RemoveDiscountCoupon;


public static class RemoveDiscountCouponEndpoint
{
    public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/remove-discount-coupon",
                async (IMediator mediator) =>
                    (await mediator.Send(new RemoveDiscountCouponCommand())))
            .WithName("RemoveDiscountCoupon");


        return group;
    }
}