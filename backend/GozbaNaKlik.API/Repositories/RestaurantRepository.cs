using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurant>> GetByOwnerId(int ownerId)
    {
        return await _context.Restaurants
            .Where(r => r.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants
            .Include(r => r.Owner)
            .ToListAsync();
    }
}
