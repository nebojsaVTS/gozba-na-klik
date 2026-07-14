using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Helpers;
using GozbaNaKlik.API.Models;
using GozbaNaKlik.API.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher = new();

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

            var (success, error, user) = await UserRegistration.TryCreateUserAsync(
                _context, request.Username, request.Email, request.Password, UserRoles.Kupac);

            if (!success)
            {
                return Conflict(new { message = error });
            }

            return Ok(new
            {
                message = "Registracija uspešna.",
                userId = user!.Id,
                username = user.Username,
                role = user.Role
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Korisnicko ime i lozinka su obavezni.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized("Neispravno korisnicko ime ili lozinka.");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
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