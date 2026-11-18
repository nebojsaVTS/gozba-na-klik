namespace GozbaNaKlik.API.Models
{
    public class RestaurantDeliveryZone
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; } = null!;

        public int SettlementId { get; set; }

        public Settlement Settlement { get; set; } = null!;

        public decimal DeliveryFee { get; set; }

        public decimal? MinimumOrderAmount { get; set; }
    }
}