using Discount.API.Models;

namespace Discount.API.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<Coupon> GetAsync(string productId);
    Task<bool> CreateAsync(Coupon coupon);
    Task<bool> UpdateAsync(Coupon coupon);
    Task<bool> DeleteAsync(string productId);
}
