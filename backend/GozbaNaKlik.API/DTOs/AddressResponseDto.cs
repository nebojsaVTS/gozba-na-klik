namespace GozbaNaKlik.API.DTOs
{
    public class AddressResponseDto
    {
        public int Id { get; set; }

        // Privremeno ostavljamo zbog postojeceg frontenda.
        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        // Novi strukturisani podaci.
        public int? StreetId { get; set; }

        public string StreetName { get; set; } = string.Empty;

        public string HouseNumber { get; set; } = string.Empty;

        public string SettlementName { get; set; } = string.Empty;

        public string MunicipalityName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}