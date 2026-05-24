using UdemyNewMicroservice.Shared;

namespace MicroCore.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon) : IRequestByServiceResult;