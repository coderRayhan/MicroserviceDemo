using Discount.Grpc.Interfaces;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ICouponRepository _repository;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(ICouponRepository repository, ILogger<DiscountService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetAsync(request.ProductId);
        if(coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
        }
        _logger.LogInformation("Discount is retrieved for ProductName: {productName},Amount : {amount}", coupon.ProductName, coupon.Amount);
        var couponResponse = new CouponRequest
        {
            Id = coupon.Id,
            ProductId = coupon.ProductId,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = (Int32)coupon.Amount
        };
        return couponResponse;
    }
}
