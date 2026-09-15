using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/menuitems")]
    public class MenuItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemDto dto)
        {
            var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);

            if (restaurant == null)
            {
                return BadRequest("Restoran sa tim ID ne postoji.");
            }

            var menuItem = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                RestaurantId = dto.RestaurantId
            };

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            var response = new MenuItemResponseDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                ImageUrl = menuItem.ImageUrl,
                RestaurantId = menuItem.RestaurantId
            };

            return Ok(response);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int restaurantId)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran sa tim ID ne postoji.");
            }

            var menuItems = await _context.MenuItems
                .AsNoTracking()
                .Where(m => m.RestaurantId == restaurantId)
                .Select(m => new MenuItemResponseDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    ImageUrl = m.ImageUrl,
                    RestaurantId = m.RestaurantId
                })
                .ToListAsync();

            return Ok(menuItems);
        }
    }
}