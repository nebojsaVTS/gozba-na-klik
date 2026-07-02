using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using GozbaNaKlik.API.Models;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RestaurantsController(AppDbContext context)
    {
        _context = context;
    }
    // TODO: Obezbediti pristup samo administratorima
    // kada bude implementirana autentikacija i autorizacija (JWT).
    [HttpPost]
    public IActionResult CreateRestaurant(CreateRestaurantDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest("Naziv restorana je obavezan.");
        }

        var owner = _context.Users.FirstOrDefault(u => u.Id == dto.OwnerId);

        if (owner == null)
        {
            return BadRequest("Vlasnik sa tim ID ne postoji.");
        }

        if (owner.Role != "Vlasnik restorana")
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

        return CreatedAtAction(nameof(CreateRestaurant), new { id = restaurant.Id }, restaurant);
    }
}