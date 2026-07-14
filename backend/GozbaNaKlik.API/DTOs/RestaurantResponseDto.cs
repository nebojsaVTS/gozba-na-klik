namespace GozbaNaKlik.API.DTOs
{
    public class RestaurantResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int OwnerId { get; set; }

        public string OwnerUsername { get; set; } = string.Empty;
    }
}
