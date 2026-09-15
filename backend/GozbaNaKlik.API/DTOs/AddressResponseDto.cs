namespace GozbaNaKlik.API.DTOs
{
    public class AddressResponseDto
    {
        public int Id { get; set; }

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}
