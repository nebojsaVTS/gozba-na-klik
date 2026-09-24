namespace GozbaNaKlik.API.Models
{
    public static class UserRoles
    {
        public const string Kupac = "Kupac";
        public const string VlasnikRestorana = "Vlasnik restorana";
        public const string Administrator = "Administrator";
        public const string Kurir = "Kurir";
        public const string RestaurantOperator = "RestaurantOperator";

        public static readonly string[] All = { Kupac, VlasnikRestorana, Administrator, Kurir, RestaurantOperator };
    }
}
