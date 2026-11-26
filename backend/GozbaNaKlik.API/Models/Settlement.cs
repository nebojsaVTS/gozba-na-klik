namespace GozbaNaKlik.API.Models
{
    public class Settlement
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int MunicipalityId { get; set; }

        public Municipality Municipality { get; set; } = null!;

        public ICollection<Street> Streets { get; set; } =
            new List<Street>();

        public ICollection<RestaurantDeliveryZone> RestaurantDeliveryZones { get; set; } =
            new List<RestaurantDeliveryZone>();

        public ICollection<Restaurant> Restaurants { get; set; } =
            new List<Restaurant>();
    }
}