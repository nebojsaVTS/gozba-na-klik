namespace GozbaNaKlik.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        // Snapshot adrese u trenutku poručivanja
        public string DeliveryStreet { get; set; } = string.Empty;
        public string DeliveryCity { get; set; } = string.Empty;
        public string DeliveryPhoneNumber { get; set; } = string.Empty;

        public string Status { get; set; } = OrderStatus.Pending;

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } =
            new List<OrderItem>();
    }
}