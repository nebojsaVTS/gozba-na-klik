namespace GozbaNaKlik.API.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}