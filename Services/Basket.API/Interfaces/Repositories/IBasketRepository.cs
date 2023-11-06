using Basket.API.Models;

namespace Basket.API.Interfaces.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
    Task DeleteBasket(string userName);
}
