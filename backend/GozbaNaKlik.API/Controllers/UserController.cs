using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(int id, UpdateProfileDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound("Korisnik ne postoji.");
        }

        user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = _passwordHasher.HashPassword(user, dto.Password);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            user.Id,
            user.Username,
            user.Email,
            user.Role
        });
    }
}