namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantDeliveryZoneRequest
    {
        public int SettlementId { get; set; }

        public decimal DeliveryFee { get; set; }

        public decimal? MinimumOrderAmount { get; set; }
    }
}