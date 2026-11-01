namespace GozbaNaKlik.API.DTOs
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public int AddressId { get; set; }

        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }
}