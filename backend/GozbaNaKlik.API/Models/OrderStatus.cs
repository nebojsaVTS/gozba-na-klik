namespace GozbaNaKlik.API.Models
{
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Rejected = "Rejected";
        public const string InPreparation = "InPreparation";
        public const string Ready = "Ready";
        public const string Completed = "Completed";

        public static readonly string[] All =
        {
            Pending,
            Accepted,
            Rejected,
            InPreparation,
            Ready,
            Completed
        };
    }
}