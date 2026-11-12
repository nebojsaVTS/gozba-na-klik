namespace GozbaNaKlik.API.Models
{
    public static class OrderFulfillmentType
    {
        public const string Delivery = "Delivery";
        public const string Pickup = "Pickup";

        public static readonly string[] All =
        {
            Delivery,
            Pickup
        };
    }
}