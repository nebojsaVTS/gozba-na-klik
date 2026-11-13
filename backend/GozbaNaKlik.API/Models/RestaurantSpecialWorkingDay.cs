namespace GozbaNaKlik.API.Models
{
    public class RestaurantSpecialWorkingDay
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public DateOnly Date { get; set; }

        public TimeOnly? OpenTime { get; set; }

        public TimeOnly? CloseTime { get; set; }

        public bool IsClosed { get; set; }
    }
}