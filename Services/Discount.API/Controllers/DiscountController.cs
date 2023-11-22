using CoreApiResponse;
using Discount.API.Interfaces.Repositories;
using Discount.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController : BaseController
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
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Coupon coupon)
    {
        var result = await _repository.CreateAsync(coupon);
        if (!result)
            return CustomResult("Coupon save failed", coupon, HttpStatusCode.BadRequest);
        return CustomResult(coupon);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Coupon coupon)
    {
        var result = await _repository.UpdateAsync(coupon);
        if (!result)
            return CustomResult("Coupon save failed", coupon, HttpStatusCode.BadRequest);
        return CustomResult("Coupon hasbeen modified");
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string ProductId)
    {
        var result = await _repository.DeleteAsync(ProductId);
        if (!result)
            return CustomResult("Delete failed", HttpStatusCode.BadRequest);
        return CustomResult("Coupon has been deleted");
    }
}
