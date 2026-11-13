using GozbaNaKlik.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Services
{
    public class RestaurantAvailabilityResult
    {
        public bool IsOpen { get; set; }

        public bool IsClosingSoon { get; set; }

        public string Message { get; set; } = string.Empty;

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }
    }

    public class RestaurantAvailabilityService
    {
        private readonly AppDbContext _context;

        public RestaurantAvailabilityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RestaurantAvailabilityResult> GetAvailabilityAsync(
            int restaurantId)
        {
            var timeZone = GetSerbiaTimeZone();

            var serbiaNow = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                timeZone
            );

            var currentDate = DateOnly.FromDateTime(serbiaNow);
            var currentTime = TimeOnly.FromDateTime(serbiaNow);
            var currentDayOfWeek = serbiaNow.DayOfWeek;

            // Poseban datum ima prednost nad redovnim radnim vremenom.
            var specialWorkingDay = await _context.RestaurantSpecialWorkingDays
                .FirstOrDefaultAsync(sw =>
                    sw.RestaurantId == restaurantId &&
                    sw.Date == currentDate
                );

            if (specialWorkingDay != null)
            {
                if (specialWorkingDay.IsClosed)
                {
                    return new RestaurantAvailabilityResult
                    {
                        IsOpen = false,
                        IsClosingSoon = false,
                        Message = "Restoran je danas zatvoren."
                    };
                }

                return BuildAvailabilityResult(
                    currentTime,
                    specialWorkingDay.OpenTime,
                    specialWorkingDay.CloseTime
                );
            }

            // Ako nema posebnog datuma, koristi se redovno radno vreme.
            var workingHours = await _context.RestaurantWorkingHours
                .FirstOrDefaultAsync(wh =>
                    wh.RestaurantId == restaurantId &&
                    wh.DayOfWeek == currentDayOfWeek
                );

            if (workingHours == null)
            {
                return new RestaurantAvailabilityResult
                {
                    IsOpen = false,
                    IsClosingSoon = false,
                    Message = "Radno vreme restorana nije definisano za danas."
                };
            }

            if (workingHours.IsClosed)
            {
                return new RestaurantAvailabilityResult
                {
                    IsOpen = false,
                    IsClosingSoon = false,
                    Message = "Restoran je danas zatvoren."
                };
            }

            return BuildAvailabilityResult(
                currentTime,
                workingHours.OpenTime,
                workingHours.CloseTime
            );
        }

        private static RestaurantAvailabilityResult BuildAvailabilityResult(
            TimeOnly currentTime,
            TimeOnly? openTime,
            TimeOnly? closeTime)
        {
            var isOpen = IsWithinWorkingHours(
                currentTime,
                openTime,
                closeTime
            );

            var isClosingSoon =
                isOpen &&
                IsClosingWithinMinutes(
                    currentTime,
                    closeTime,
                    30
                );

            string message;

            if (!isOpen)
            {
                message =
                    $"Restoran je trenutno zatvoren. Danas radi od {FormatTime(openTime)} do {FormatTime(closeTime)}.";
            }
            else if (isClosingSoon)
            {
                message =
                    $"Restoran se uskoro zatvara. Radno vreme danas je do {FormatTime(closeTime)}.";
            }
            else
            {
                message = "Restoran je otvoren.";
            }

            return new RestaurantAvailabilityResult
            {
                IsOpen = isOpen,
                IsClosingSoon = isClosingSoon,
                OpenTime = openTime,
                CloseTime = closeTime,
                Message = message
            };
        }

        private static bool IsWithinWorkingHours(
            TimeOnly currentTime,
            TimeOnly? openTime,
            TimeOnly? closeTime)
        {
            if (!openTime.HasValue || !closeTime.HasValue)
            {
                return false;
            }

            // Normalno radno vreme, npr. 08:00 - 20:00.
            if (openTime.Value < closeTime.Value)
            {
                return currentTime >= openTime.Value &&
                       currentTime < closeTime.Value;
            }

            // Rad preko ponoći, npr. 18:00 - 02:00.
            if (openTime.Value > closeTime.Value)
            {
                return currentTime >= openTime.Value ||
                       currentTime < closeTime.Value;
            }

            return false;
        }

        private static bool IsClosingWithinMinutes(
            TimeOnly currentTime,
            TimeOnly? closeTime,
            int minutes)
        {
            if (!closeTime.HasValue)
            {
                return false;
            }

            var currentMinutes =
                currentTime.Hour * 60 + currentTime.Minute;

            var closeMinutes =
                closeTime.Value.Hour * 60 + closeTime.Value.Minute;

            var minutesUntilClose = closeMinutes - currentMinutes;

            // Ako restoran radi preko ponoći,
            // npr. 18:00 - 02:00.
            if (minutesUntilClose < 0)
            {
                minutesUntilClose += 24 * 60;
            }

            return minutesUntilClose >= 0 &&
                   minutesUntilClose <= minutes;
        }

        private static string FormatTime(TimeOnly? time)
        {
            return time?.ToString("HH:mm") ?? "";
        }

        private static TimeZoneInfo GetSerbiaTimeZone()
        {
            try
            {
                // Linux / Docker
                return TimeZoneInfo.FindSystemTimeZoneById("Europe/Belgrade");
            }
            catch (TimeZoneNotFoundException)
            {
                // Windows
                return TimeZoneInfo.FindSystemTimeZoneById(
                    "Central European Standard Time"
                );
            }
        }
    }
}