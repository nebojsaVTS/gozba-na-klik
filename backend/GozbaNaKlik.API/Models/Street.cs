namespace GozbaNaKlik.API.Models
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int SettlementId { get; set; }

        public Settlement Settlement { get; set; } = null!;

        public ICollection<Address> Addresses { get; set; } =
            new List<Address>();
    }
}