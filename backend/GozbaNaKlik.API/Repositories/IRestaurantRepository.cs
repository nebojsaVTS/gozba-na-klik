using GozbaNaKlik.API.Models;

namespace GozbaNaKlik.API.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetByOwnerId(int ownerId);
    Task<List<Restaurant>> GetAllAsync();
}
