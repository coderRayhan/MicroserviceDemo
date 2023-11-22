using Discount.Grpc.Models;

namespace Discount.Grpc.Interfaces;

public interface ICouponRepository
{
    Task<Coupon> GetAsync(string productId);
    Task<bool> CreateAsync(Coupon coupon);
    Task<bool> UpdateAsync(Coupon coupon);
    Task<bool> DeleteAsync(string productId);
}
