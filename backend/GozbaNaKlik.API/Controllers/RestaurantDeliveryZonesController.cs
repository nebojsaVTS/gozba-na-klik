using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/delivery-zones")]
    public class RestaurantDeliveryZonesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantDeliveryZonesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/restaurants/1/delivery-zones
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeliveryZones(int restaurantId)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restaurant not found.");
            }

            var zones = await _context.RestaurantDeliveryZones
                .Where(dz => dz.RestaurantId == restaurantId)
                .Include(dz => dz.Settlement)
                    .ThenInclude(s => s.Municipality)
                .OrderBy(dz => dz.Settlement.Name)
                .Select(dz => new
                {
                    dz.Id,
                    dz.RestaurantId,
                    dz.SettlementId,
                    SettlementName = dz.Settlement.Name,
                    MunicipalityName = dz.Settlement.Municipality.Name,
                    dz.DeliveryFee,
                    dz.MinimumOrderAmount
                })
                .ToListAsync();

            return Ok(zones);
        }

        // POST: api/restaurants/1/delivery-zones
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDeliveryZone(
            int restaurantId,
            RestaurantDeliveryZoneRequest request)
        {
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Id == restaurantId);

            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }

            if (!restaurant.SupportsDelivery)
            {
                return BadRequest("This restaurant does not support delivery.");
            }

            var settlementExists = await _context.Settlements
                .AnyAsync(s => s.Id == request.SettlementId);

            if (!settlementExists)
            {
                return BadRequest("Settlement not found.");
            }

            if (request.DeliveryFee < 0)
            {
                return BadRequest("Delivery fee cannot be negative.");
            }

            if (request.MinimumOrderAmount < 0)
            {
                return BadRequest("Minimum order amount cannot be negative.");
            }

            var zoneAlreadyExists = await _context.RestaurantDeliveryZones
                .AnyAsync(dz =>
                    dz.RestaurantId == restaurantId &&
                    dz.SettlementId == request.SettlementId);

            if (zoneAlreadyExists)
            {
                return BadRequest(
                    "Delivery zone for this settlement already exists.");
            }

            var zone = new RestaurantDeliveryZone
            {
                RestaurantId = restaurantId,
                SettlementId = request.SettlementId,
                DeliveryFee = request.DeliveryFee,
                MinimumOrderAmount = request.MinimumOrderAmount
            };

            _context.RestaurantDeliveryZones.Add(zone);
            await _context.SaveChangesAsync();

            var createdZone = await _context.RestaurantDeliveryZones
                .Where(dz => dz.Id == zone.Id)
                .Include(dz => dz.Settlement)
                    .ThenInclude(s => s.Municipality)
                .Select(dz => new
                {
                    dz.Id,
                    dz.RestaurantId,
                    dz.SettlementId,
                    SettlementName = dz.Settlement.Name,
                    MunicipalityName = dz.Settlement.Municipality.Name,
                    dz.DeliveryFee,
                    dz.MinimumOrderAmount
                })
                .FirstAsync();

            return CreatedAtAction(
                nameof(GetDeliveryZones),
                new { restaurantId },
                createdZone);
        }

        // PUT: api/restaurants/1/delivery-zones/3
        [HttpPut("{zoneId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDeliveryZone(
            int restaurantId,
            int zoneId,
            RestaurantDeliveryZoneRequest request)
        {
            var zone = await _context.RestaurantDeliveryZones
                .FirstOrDefaultAsync(dz =>
                    dz.Id == zoneId &&
                    dz.RestaurantId == restaurantId);

            if (zone == null)
            {
                return NotFound("Delivery zone not found.");
            }

            var settlementExists = await _context.Settlements
                .AnyAsync(s => s.Id == request.SettlementId);

            if (!settlementExists)
            {
                return BadRequest("Settlement not found.");
            }

            if (request.DeliveryFee < 0)
            {
                return BadRequest("Delivery fee cannot be negative.");
            }

            if (request.MinimumOrderAmount < 0)
            {
                return BadRequest("Minimum order amount cannot be negative.");
            }

            var duplicateZoneExists = await _context.RestaurantDeliveryZones
                .AnyAsync(dz =>
                    dz.RestaurantId == restaurantId &&
                    dz.SettlementId == request.SettlementId &&
                    dz.Id != zoneId);

            if (duplicateZoneExists)
            {
                return BadRequest(
                    "Delivery zone for this settlement already exists.");
            }

            zone.SettlementId = request.SettlementId;
            zone.DeliveryFee = request.DeliveryFee;
            zone.MinimumOrderAmount = request.MinimumOrderAmount;

            await _context.SaveChangesAsync();

            var updatedZone = await _context.RestaurantDeliveryZones
                .Where(dz => dz.Id == zone.Id)
                .Include(dz => dz.Settlement)
                    .ThenInclude(s => s.Municipality)
                .Select(dz => new
                {
                    dz.Id,
                    dz.RestaurantId,
                    dz.SettlementId,
                    SettlementName = dz.Settlement.Name,
                    MunicipalityName = dz.Settlement.Municipality.Name,
                    dz.DeliveryFee,
                    dz.MinimumOrderAmount
                })
                .FirstAsync();

            return Ok(updatedZone);
        }

        // DELETE: api/restaurants/1/delivery-zones/3
        [HttpDelete("{zoneId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDeliveryZone(
            int restaurantId,
            int zoneId)
        {
            var zone = await _context.RestaurantDeliveryZones
                .FirstOrDefaultAsync(dz =>
                    dz.Id == zoneId &&
                    dz.RestaurantId == restaurantId);

            if (zone == null)
            {
                return NotFound("Delivery zone not found.");
            }

            _context.RestaurantDeliveryZones.Remove(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}