namespace GozbaNaKlik.API.Models
{
    public class Municipality
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Settlement> Settlements { get; set; } =
            new List<Settlement>();
    }
}