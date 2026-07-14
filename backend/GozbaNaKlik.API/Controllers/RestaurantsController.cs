using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using GozbaNaKlik.API.Models;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantsController(AppDbContext context, IRestaurantRepository restaurantRepository)
    {
        _context = context;
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetByOwner([FromQuery] int ownerId)
    {
        var restaurants = await _restaurantRepository.GetByOwnerId(ownerId);

        var result = restaurants.Select(r => new RestaurantListItemDto
        {
            Name = r.Name,
            CoverPhotoUrl = r.CoverPhotoUrl
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            return NotFound("Restoran ne postoji.");
        }

        var owner = await _context.Users.FindAsync(restaurant.OwnerId);

        var response = new RestaurantResponseDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            PhoneNumber = restaurant.PhoneNumber,
            OwnerId = restaurant.OwnerId,
            OwnerUsername = owner?.Username ?? string.Empty
        };

        return Ok(response);
    }

    // TODO: Obezbediti pristup samo administratorima
    // kada bude implementirana autentikacija i autorizacija (JWT).

    [HttpPost]
    public IActionResult CreateRestaurant(CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var owner = _context.Users.FirstOrDefault(u => u.Id == dto.OwnerId);

        if (owner == null)
        {
            return BadRequest("Vlasnik sa tim ID ne postoji.");
        }

        if (owner.Role != UserRoles.VlasnikRestorana)
        {
            return BadRequest("Izabrani korisnik nije vlasnik restorana.");
        }

        var restaurant = new Restaurant
        {
            Name = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            OwnerId = dto.OwnerId
        };

        _context.Restaurants.Add(restaurant);
        _context.SaveChanges();

        var response = new RestaurantResponseDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            PhoneNumber = restaurant.PhoneNumber,
            OwnerId = restaurant.OwnerId,
            OwnerUsername = owner.Username
        };

        return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, response);
    }

    // TODO: Obezbediti pristup samo administratorima
    // kada bude implementirana autentikacija i autorizacija (JWT).
    [HttpPut("{id}")]
    public IActionResult UpdateRestaurant(int id, UpdateRestaurantDto dto)
    {
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

        if (restaurant == null)
        {
            return NotFound("Restoran ne postoji.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        restaurant.Name = dto.Name;
        restaurant.Address = dto.Address;
        restaurant.PhoneNumber = dto.PhoneNumber;

        _context.SaveChanges();

        return Ok(restaurant);
    }
    // TODO: Obezbediti pristup samo administratorima
    // kada bude implementirana autentikacija i autorizacija (JWT).
    [HttpDelete("{id}")]
    public IActionResult DeleteRestaurant(int id)
    {
        var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id == id);

        if (restaurant == null)
        {
            return NotFound("Restoran ne postoji.");
        }

        _context.Restaurants.Remove(restaurant);
        _context.SaveChanges();

        return Ok("Restoran je uspešno obrisan.");
    }
}