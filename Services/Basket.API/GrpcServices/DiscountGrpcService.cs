using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
    {
        _discountService = discountService;
    }

    public async Task<CouponRequest> GetDiscount(string productId)
    {
        return await _discountService.GetDiscountAsync(new GetDiscountRequest { ProductId = productId });
    }
}
