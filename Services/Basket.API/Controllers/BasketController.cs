using Basket.API.Interfaces.Repositories;
using Basket.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("{userName}")]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBasket([FromRoute] string userName)
    {
        try
        {
            return Ok(await _repository.GetBasket(userName));
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
    {
        try
        {
            var cart = await _repository.UpdateBasket(basket);
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{userName}")]
    public async Task<IActionResult> DeleteBasket([FromRoute] string userName)
    {
        try
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
