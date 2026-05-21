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
    public ActionResult<List<User>> GetAllUsers()
    {
        List<User> users = _context.Users.ToList();

        return Ok(users);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }
}