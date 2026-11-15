namespace GozbaNaKlik.API.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public int? StreetId { get; set; }

        public Street? StreetEntity { get; set; }

        public string HouseNumber { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}