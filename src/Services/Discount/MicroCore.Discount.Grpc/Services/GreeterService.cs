using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MicroCore.Discount.Grpc;
using MicroCore.Discount.Grpc.Data;
using MicroCore.Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
namespace MicroCore.Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<DiscountModel> GetDiscountByCode(GetDiscountByCodeRequest request, ServerCallContext context)
    {
        var discount = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.Code == request.Code);

        if (discount is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Code='{request.Code}' is not found."));
        }

        logger.LogInformation("Discount is retrieved for Code: {code}, Rate: {rate}", discount.Code, discount.Rate);

        return new DiscountModel
        {
            Id = discount.Id.ToString(),
            UserId = discount.UserId.ToString(),
            Rate = discount.Rate,
            Code = discount.Code,
            Created = Timestamp.FromDateTime(discount.Created.ToUniversalTime()),
            Updated = discount.Updated.HasValue ? Timestamp.FromDateTime(discount.Updated.Value.ToUniversalTime()) : new Timestamp(),
            Expired = Timestamp.FromDateTime(discount.Expired.ToUniversalTime())
        };
    }

    public override async Task<DiscountModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var newDiscount = new Coupon
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(request.Discount.UserId),
            Rate = request.Discount.Rate,
            Code = request.Discount.Code,
            Created = DateTime.UtcNow,
            Updated = null,
            Expired = request.Discount.Expired.ToDateTime()
        };

        dbContext.Coupons.Add(newDiscount);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. Code: {Code}", newDiscount.Code);

        request.Discount.Id = newDiscount.Id.ToString();
        request.Discount.Created = Timestamp.FromDateTime(newDiscount.Created.ToUniversalTime());

        return request.Discount;
    }
}
