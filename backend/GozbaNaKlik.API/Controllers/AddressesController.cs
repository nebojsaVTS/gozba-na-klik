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
            .Include(a => a.StreetEntity)
                .ThenInclude(s => s!.Settlement)
                    .ThenInclude(s => s.Municipality)
            .ToListAsync();

        var result = addresses.Select(MapToResponseDto);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var address = await _context.Addresses
            .Include(a => a.StreetEntity)
                .ThenInclude(s => s!.Settlement)
                    .ThenInclude(s => s.Municipality)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (address == null)
        {
            return NotFound("Adresa ne postoji.");
        }

        return Ok(MapToResponseDto(address));
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

        var street = await _context.Streets
            .Include(s => s.Settlement)
                .ThenInclude(s => s.Municipality)
            .FirstOrDefaultAsync(s => s.Id == dto.StreetId);

        if (street == null)
        {
            return BadRequest("Izabrana ulica ne postoji.");
        }

        var houseNumber = dto.HouseNumber.Trim();

        if (string.IsNullOrWhiteSpace(houseNumber))
        {
            return BadRequest("Broj kuće ili zgrade je obavezan.");
        }

        var address = new Address
        {
            StreetId = street.Id,
            StreetEntity = street,

            HouseNumber = houseNumber,
            PhoneNumber = dto.PhoneNumber.Trim(),
            UserId = dto.UserId,

            // Privremeno cuvamo i stare tekstualne vrednosti
            // dok frontend i ostatak sistema potpuno ne predju
            // na strukturisani model.
            Street = street.Name,
            City = street.Settlement.Name
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = address.Id },
            MapToResponseDto(address)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, UpdateAddressDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var address = await _context.Addresses
            .FirstOrDefaultAsync(a => a.Id == id);

        if (address == null)
        {
            return NotFound("Adresa ne postoji.");
        }

        var street = await _context.Streets
            .Include(s => s.Settlement)
                .ThenInclude(s => s.Municipality)
            .FirstOrDefaultAsync(s => s.Id == dto.StreetId);

        if (street == null)
        {
            return BadRequest("Izabrana ulica ne postoji.");
        }

        var houseNumber = dto.HouseNumber.Trim();

        if (string.IsNullOrWhiteSpace(houseNumber))
        {
            return BadRequest("Broj kuće ili zgrade je obavezan.");
        }

        address.StreetId = street.Id;
        address.StreetEntity = street;
        address.HouseNumber = houseNumber;
        address.PhoneNumber = dto.PhoneNumber.Trim();

        // Privremena kompatibilnost sa starim sistemom.
        address.Street = street.Name;
        address.City = street.Settlement.Name;

        await _context.SaveChangesAsync();

        return Ok(MapToResponseDto(address));
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

    private static AddressResponseDto MapToResponseDto(Address address)
    {
        var streetName =
            address.StreetEntity?.Name
            ?? address.Street;

        var settlementName =
            address.StreetEntity?.Settlement?.Name
            ?? address.City;

        var municipalityName =
            address.StreetEntity?.Settlement?.Municipality?.Name
            ?? string.Empty;

        return new AddressResponseDto
        {
            Id = address.Id,

            // Stari response podaci za trenutni frontend.
            Street = streetName,
            City = settlementName,

            // Novi strukturisani podaci.
            StreetId = address.StreetId,
            StreetName = streetName,
            HouseNumber = address.HouseNumber,
            SettlementName = settlementName,
            MunicipalityName = municipalityName,

            PhoneNumber = address.PhoneNumber,
            UserId = address.UserId
        };
    }
}