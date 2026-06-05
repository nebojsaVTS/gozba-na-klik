using GozbaNaKlik.API.Data;
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
    public IActionResult CreateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User object is null");
        }

        if (string.IsNullOrEmpty(user.Username) ||
            string.IsNullOrEmpty(user.Password) ||
            string.IsNullOrEmpty(user.Email) ||
            string.IsNullOrEmpty(user.Role))
        {
            return BadRequest("Sva polja su obavezna");
        }

        // opcionalno: provera da li user već postoji
        var existingUser = _context.Users
            .FirstOrDefault(u => u.Username == user.Username);

        if (existingUser != null)
        {
            return BadRequest("Korisnik već postoji");
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        // vraćamo bez password-a (bezbednije)
        return Ok(new
        {
            user.Id,
            user.Username,
            user.Email,
            user.Role
        });
    }

}