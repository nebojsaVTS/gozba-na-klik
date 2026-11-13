using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using GozbaNaKlik.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/working-hours")]
    public class RestaurantWorkingHoursController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RestaurantAvailabilityService _availabilityService;

        public RestaurantWorkingHoursController(
            AppDbContext context,
            RestaurantAvailabilityService availabilityService)
        {
            _context = context;
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantWorkingHoursDto>>> GetWorkingHours(
            int restaurantId)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran nije pronađen.");
            }

            var workingHours = await _context.RestaurantWorkingHours
                .Where(wh => wh.RestaurantId == restaurantId)
                .OrderBy(wh => wh.DayOfWeek)
                .Select(wh => new RestaurantWorkingHoursDto
                {
                    DayOfWeek = (int)wh.DayOfWeek,
                    OpenTime = wh.OpenTime,
                    CloseTime = wh.CloseTime,
                    IsClosed = wh.IsClosed
                })
                .ToListAsync();

            return Ok(workingHours);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWorkingHours(
            int restaurantId,
            List<RestaurantWorkingHoursDto> workingHoursDtos)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran nije pronađen.");
            }

            if (workingHoursDtos == null || workingHoursDtos.Count == 0)
            {
                return BadRequest("Radno vreme nije prosleđeno.");
            }

            if (workingHoursDtos.Any(x => x.DayOfWeek < 0 || x.DayOfWeek > 6))
            {
                return BadRequest("Dan u nedelji mora biti između 0 i 6.");
            }

            if (workingHoursDtos
                .GroupBy(x => x.DayOfWeek)
                .Any(group => group.Count() > 1))
            {
                return BadRequest("Isti dan u nedelji ne može biti unet više puta.");
            }

            foreach (var dto in workingHoursDtos)
            {
                if (!dto.IsClosed)
                {
                    if (!dto.OpenTime.HasValue || !dto.CloseTime.HasValue)
                    {
                        return BadRequest(
                            "Za radni dan moraju biti uneti vreme otvaranja i zatvaranja.");
                    }

                    if (dto.OpenTime.Value == dto.CloseTime.Value)
                    {
                        return BadRequest(
                            "Vreme otvaranja i zatvaranja ne mogu biti ista.");
                    }
                }
            }

            var existingWorkingHours = await _context.RestaurantWorkingHours
                .Where(wh => wh.RestaurantId == restaurantId)
                .ToListAsync();

            _context.RestaurantWorkingHours.RemoveRange(existingWorkingHours);

            var newWorkingHours = workingHoursDtos.Select(dto =>
                new RestaurantWorkingHours
                {
                    RestaurantId = restaurantId,
                    DayOfWeek = (DayOfWeek)dto.DayOfWeek,
                    OpenTime = dto.IsClosed ? null : dto.OpenTime,
                    CloseTime = dto.IsClosed ? null : dto.CloseTime,
                    IsClosed = dto.IsClosed
                });

            await _context.RestaurantWorkingHours.AddRangeAsync(newWorkingHours);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/api/restaurants/{restaurantId}/special-working-days")]
        public async Task<ActionResult<IEnumerable<RestaurantSpecialWorkingDayDto>>> GetSpecialWorkingDays(
            int restaurantId)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran nije pronađen.");
            }

            var specialWorkingDays = await _context.RestaurantSpecialWorkingDays
                .Where(sw => sw.RestaurantId == restaurantId)
                .OrderBy(sw => sw.Date)
                .Select(sw => new RestaurantSpecialWorkingDayDto
                {
                    Date = sw.Date,
                    OpenTime = sw.OpenTime,
                    CloseTime = sw.CloseTime,
                    IsClosed = sw.IsClosed
                })
                .ToListAsync();

            return Ok(specialWorkingDays);
        }

        [HttpPut("/api/restaurants/{restaurantId}/special-working-days")]
        public async Task<IActionResult> UpdateSpecialWorkingDays(
            int restaurantId,
            List<RestaurantSpecialWorkingDayDto> specialWorkingDayDtos)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran nije pronađen.");
            }

            if (specialWorkingDayDtos == null)
            {
                return BadRequest("Posebni radni dani nisu prosleđeni.");
            }

            if (specialWorkingDayDtos
                .GroupBy(x => x.Date)
                .Any(group => group.Count() > 1))
            {
                return BadRequest("Isti datum ne može biti unet više puta.");
            }

            foreach (var dto in specialWorkingDayDtos)
            {
                if (!dto.IsClosed)
                {
                    if (!dto.OpenTime.HasValue || !dto.CloseTime.HasValue)
                    {
                        return BadRequest(
                            "Za poseban radni dan moraju biti uneti vreme otvaranja i zatvaranja.");
                    }

                    if (dto.OpenTime.Value == dto.CloseTime.Value)
                    {
                        return BadRequest(
                            "Vreme otvaranja i zatvaranja ne mogu biti ista.");
                    }
                }
            }

            var existingSpecialWorkingDays = await _context.RestaurantSpecialWorkingDays
                .Where(sw => sw.RestaurantId == restaurantId)
                .ToListAsync();

            _context.RestaurantSpecialWorkingDays.RemoveRange(
                existingSpecialWorkingDays);

            var newSpecialWorkingDays = specialWorkingDayDtos.Select(dto =>
                new RestaurantSpecialWorkingDay
                {
                    RestaurantId = restaurantId,
                    Date = dto.Date,
                    OpenTime = dto.IsClosed ? null : dto.OpenTime,
                    CloseTime = dto.IsClosed ? null : dto.CloseTime,
                    IsClosed = dto.IsClosed
                });

            await _context.RestaurantSpecialWorkingDays.AddRangeAsync(
                newSpecialWorkingDays);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("/api/restaurants/{restaurantId}/availability")]
        public async Task<IActionResult> GetAvailability(int restaurantId)
        {
            var restaurantExists = await _context.Restaurants
                .AnyAsync(r => r.Id == restaurantId);

            if (!restaurantExists)
            {
                return NotFound("Restoran nije pronađen.");
            }

            var availability =
                await _availabilityService.GetAvailabilityAsync(restaurantId);

            return Ok(availability);
        }
    }
}