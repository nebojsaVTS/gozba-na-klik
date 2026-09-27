using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/restaurant-operator-assignments")]
public class RestaurantOperatorAssignmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RestaurantOperatorAssignmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assignments = await _context.RestaurantOperatorAssignments
            .Include(a => a.User)
            .Include(a => a.Restaurant)
            .ToListAsync();

        var result = assignments.Select(a => new RestaurantOperatorAssignmentResponseDto
        {
            Id = a.Id,
            UserId = a.UserId,
            Username = a.User.Username,
            RestaurantId = a.RestaurantId,
            RestaurantName = a.Restaurant.Name
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AssignOperator(AssignOperatorDto dto)
    {
        var user = await _context.Users.FindAsync(dto.UserId);

        if (user == null)
        {
            return BadRequest("Korisnik sa tim ID ne postoji.");
        }

        if (user.Role != UserRoles.RestaurantOperator)
        {
            return BadRequest("Izabrani korisnik nema rolu RestaurantOperator.");
        }

        var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);

        if (restaurant == null)
        {
            return BadRequest("Restoran sa tim ID ne postoji.");
        }

        bool alreadyAssigned = await _context.RestaurantOperatorAssignments
            .AnyAsync(a => a.UserId == dto.UserId && a.RestaurantId == dto.RestaurantId);

        if (alreadyAssigned)
        {
            return BadRequest("Ovaj operater je vec dodeljen ovom restoranu.");
        }

        var assignment = new RestaurantOperatorAssignment
        {
            UserId = dto.UserId,
            RestaurantId = dto.RestaurantId
        };

        _context.RestaurantOperatorAssignments.Add(assignment);
        await _context.SaveChangesAsync();

        var response = new RestaurantOperatorAssignmentResponseDto
        {
            Id = assignment.Id,
            UserId = user.Id,
            Username = user.Username,
            RestaurantId = restaurant.Id,
            RestaurantName = restaurant.Name
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAssignment(int id)
    {
        var assignment = await _context.RestaurantOperatorAssignments.FindAsync(id);

        if (assignment == null)
        {
            return NotFound("Dodela ne postoji.");
        }

        _context.RestaurantOperatorAssignments.Remove(assignment);
        await _context.SaveChangesAsync();

        return Ok("Dodela je uspešno uklonjena.");
    }
}
