namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? CoverPhotoUrl { get; set; }
    }
}
