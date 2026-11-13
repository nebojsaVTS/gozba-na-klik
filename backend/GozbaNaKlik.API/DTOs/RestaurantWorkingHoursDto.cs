namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantWorkingHoursDto
    {
        public int DayOfWeek { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
}