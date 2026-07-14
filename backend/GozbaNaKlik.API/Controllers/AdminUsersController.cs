using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Helpers;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminUsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/admin/users
    [HttpGet]
    public ActionResult GetAllUsers()
    {
        var users = _context.Users
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.Role
            })
            .ToList();

        return Ok(users);
    }

    // POST: api/admin/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateAdminUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!UserRoles.All.Contains(dto.Role))
        {
            return BadRequest($"Nepoznata rola. Dozvoljene role su: {string.Join(", ", UserRoles.All)}");
        }

        var (success, error, user) = await UserRegistration.TryCreateUserAsync(
            _context, dto.Username, dto.Email, dto.Password, dto.Role);

        if (!success)
        {
            return Conflict(new { message = error });
        }

        // vraćamo bez password-a (bezbednije)
        return Ok(new
        {
            user!.Id,
            user.Username,
            user.Email,
            user.Role
        });
    }
}