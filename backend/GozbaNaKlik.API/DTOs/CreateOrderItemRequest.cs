namespace GozbaNaKlik.API.DTOs
{
    public class CreateOrderItemRequest
    {
        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public List<string> SelectedSides { get; set; } = new();

        public string? Note { get; set; }
    }
}