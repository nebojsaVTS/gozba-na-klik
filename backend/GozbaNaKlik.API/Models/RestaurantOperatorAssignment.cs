namespace GozbaNaKlik.API.Models
{
    public class RestaurantOperatorAssignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
    }
}