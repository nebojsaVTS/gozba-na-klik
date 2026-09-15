using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressesController : ControllerBase
{
    private readonly AppDbContext _context;

    public AddressesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetByUser([FromQuery] int userId)
    {
        var addresses = await _context.Addresses
            .Where(a => a.UserId == userId)
            .ToListAsync();

        var result = addresses.Select(a => new AddressResponseDto
        {
            Id = a.Id,
            Street = a.Street,
            City = a.City,
            UserId = a.UserId
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var address = await _context.Addresses.FindAsync(id);

        if (address == null)
        {
            return NotFound("Adresa ne postoji.");
        }

        return Ok(new AddressResponseDto
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            UserId = address.UserId
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddress(CreateAddressDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.FindAsync(dto.UserId);

        if (user == null)
        {
            return BadRequest("Korisnik sa tim ID ne postoji.");
        }

        var address = new Address
        {
            Street = dto.Street,
            City = dto.City,
            UserId = dto.UserId
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var response = new AddressResponseDto
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            UserId = address.UserId
        };

        return CreatedAtAction(nameof(GetById), new { id = address.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, UpdateAddressDto dto)
    {
        var address = await _context.Addresses.FindAsync(id);

        if (address == null)
        {
            return NotFound("Adresa ne postoji.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        address.Street = dto.Street;
        address.City = dto.City;

        await _context.SaveChangesAsync();

        return Ok(new AddressResponseDto
        {
            Id = address.Id,
            Street = address.Street,
            City = address.City,
            UserId = address.UserId
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _context.Addresses.FindAsync(id);

        if (address == null)
        {
            return NotFound("Adresa ne postoji.");
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return Ok("Adresa je uspešno obrisana.");
    }
}
