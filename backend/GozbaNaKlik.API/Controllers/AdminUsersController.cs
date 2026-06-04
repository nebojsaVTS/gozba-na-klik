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


}