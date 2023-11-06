using Discount.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly ICouponRepository _repository;

    public DiscountController(ICouponRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    [Route("{productId}")]
    public async Task<IActionResult> GetDiscountAsync([FromRoute] string productId)
    {
        var coupon = await _repository.GetAsync(productId);
        return Ok(coupon);
    }
}
