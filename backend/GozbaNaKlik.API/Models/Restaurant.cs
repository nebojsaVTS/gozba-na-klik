namespace GozbaNaKlik.API.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string? CoverPhotoUrl { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
