namespace GozbaNaKlik.API.DTOs
{
    public class UpdateRestaurantDto
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int SettlementId { get; set; }

        public bool SupportsDelivery { get; set; }

        public bool SupportsPickup { get; set; }
    }
}