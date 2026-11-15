using GozbaNaKlik.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/locations/municipalities
        [HttpGet("municipalities")]
        public async Task<IActionResult> GetMunicipalities()
        {
            var municipalities = await _context.Municipalities
                .OrderBy(m => m.Name)
                .Select(m => new
                {
                    m.Id,
                    m.Name
                })
                .ToListAsync();

            return Ok(municipalities);
        }

        // GET: api/locations/municipalities/1/settlements
        [HttpGet("municipalities/{municipalityId}/settlements")]
        public async Task<IActionResult> GetSettlements(int municipalityId)
        {
            var municipalityExists = await _context.Municipalities
                .AnyAsync(m => m.Id == municipalityId);

            if (!municipalityExists)
            {
                return NotFound("Opština nije pronađena.");
            }

            var settlements = await _context.Settlements
                .Where(s => s.MunicipalityId == municipalityId)
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Id,
                    s.Name
                })
                .ToListAsync();

            return Ok(settlements);
        }

        // GET: api/locations/settlements/1001/streets
        [HttpGet("settlements/{settlementId}/streets")]
        public async Task<IActionResult> GetStreets(int settlementId)
        {
            var settlementExists = await _context.Settlements
                .AnyAsync(s => s.Id == settlementId);

            if (!settlementExists)
            {
                return NotFound("Naselje nije pronađeno.");
            }

            var streets = await _context.Streets
                .Where(s => s.SettlementId == settlementId)
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Id,
                    s.Name
                })
                .ToListAsync();

            return Ok(streets);
        }
    }
}