using AutoMapper;
using Discount.Grpc.Interfaces;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly ICouponRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(ICouponRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetAsync(request.ProductId);
        if(coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
        }
        _logger.LogInformation("Discount is retrieved for ProductName: {productName},Amount : {amount}", coupon.ProductName, coupon.Amount);
        //var couponResponse = new CouponRequest
        //{
        //    Id = coupon.Id,
        //    ProductId = coupon.ProductId,
        //    ProductName = coupon.ProductName,
        //    Description = coupon.Description,
        //    Amount = (Int32)coupon.Amount
        //};

        var couponResponse = _mapper.Map<CouponRequest>(coupon);
        return couponResponse;
    }

    public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request);
        var result = await _repository.CreateAsync(coupon);
        if (!result)
            _logger.LogInformation("Discount create failed for Product: {productName}", coupon.ProductName);
        else
            _logger.LogInformation("Discount create Success for Product: {productName}", coupon.ProductName);
        return _mapper.Map<CouponRequest>(coupon);
    }
    public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request);
        var result = await _repository.UpdateAsync(coupon);
        if (result)
            _logger.LogInformation("Discount updated succefully for product: {productName}", coupon.ProductName);
        else
            _logger.LogInformation("Discount update failed for product: {productName}", coupon.ProductName);
        return _mapper.Map<CouponRequest>(coupon);
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var isDeleted = await _repository.DeleteAsync(request.ProductId);
        if(isDeleted)
            _logger.LogInformation("Discount deleted succesfully for product Id: {productId}", request.ProductId);
        else
            _logger.LogInformation("Discount deletion failed");
        return new DeleteDiscountResponse { Success = isDeleted };
    }
}
