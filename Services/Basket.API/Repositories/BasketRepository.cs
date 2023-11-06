using Basket.API.Interfaces.Repositories;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _radisCache;
    public BasketRepository(IDistributedCache radisCache)
    {
        _radisCache = radisCache;
    }
    public async Task DeleteBasket(string userName)
    {
        await _radisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await _radisCache.GetStringAsync(userName);
        if (!string.IsNullOrEmpty(basket)) 
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        else
            return null;

    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        var stringBasket = JsonConvert.SerializeObject(basket);
        await _radisCache.SetStringAsync(basket.UserName, stringBasket);
        return await GetBasket(basket.UserName);
    }
}
