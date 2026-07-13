using GozbaNaKlik.API.Data;
using Microsoft.AspNetCore.Mvc;
using GozbaNaKlik.API.DTOs;

namespace GozbaNaKlik.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Username) || 
               string.IsNullOrWhiteSpace(request.Password)) 
            {
                return BadRequest("Korisnicko ime i lozinka su obavezni.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if(user == null)
            {
                return Unauthorized("Neispravno korisnicko ime ili lozinka.");
            }
            
            if(user.Password != request.Password)
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
