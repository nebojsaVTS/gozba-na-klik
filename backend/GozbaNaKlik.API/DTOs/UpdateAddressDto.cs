using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class UpdateAddressDto
    {
        [Required(ErrorMessage = "Ulica je obavezna.")]
        public int StreetId { get; set; }

        [Required(ErrorMessage = "Broj kuće ili zgrade je obavezan.")]
        [StringLength(
            10,
            ErrorMessage = "Broj kuće ili zgrade može imati najviše 10 karaktera."
        )]
        public string HouseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [RegularExpression(
            @"^\+381\d{8,9}$",
            ErrorMessage = "Broj telefona mora sadržati 8 ili 9 cifara nakon +381."
        )]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}