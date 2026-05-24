using FluentValidation;

namespace MicroCore.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
{
    public ApplyDiscountCouponCommandValidator()
    {
        RuleFor(x => x.Coupon).NotEmpty().WithMessage("{PropertyName} is required");
    }
}