namespace GozbaNaKlik.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // ID originalnog jela u trenutku poručivanja
        public int MenuItemId { get; set; }

        // Snapshot podaci
        public string ItemName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // Priloge sačuvati kao JSON string
        public string SelectedSides { get; set; } = "[]";

        public string? Note { get; set; }
    }
}