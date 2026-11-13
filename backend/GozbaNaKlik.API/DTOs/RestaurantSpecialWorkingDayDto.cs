namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantSpecialWorkingDayDto
    {
        public DateOnly Date { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
}