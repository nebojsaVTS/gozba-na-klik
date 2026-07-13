using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using GozbaNaKlik.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool exists = await _context.Users.AnyAsync(u =>
                u.Username == request.Username || u.Email == request.Email);

            if (exists)
            {
                return Conflict(new { message = "Korisnik sa tim username-om ili email-om već postoji." });
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = "Kupac"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Registracija uspešna.",
                userId = user.Id,
                username = user.Username,
                role = user.Role
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Korisnicko ime i lozinka su obavezni.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized("Neispravno korisnicko ime ili lozinka.");
            }

            if (user.Password != request.Password)
            {
                return Unauthorized("Neispravno korisnicko ime ili lozinka");
            }

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }
    }
}