using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class CreateAddressDto
    {
        [Required(ErrorMessage = "Ulica je obavezna!")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grad je obavezan!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        [RegularExpression(
        @"^\+381\d{8,9}$",
        ErrorMessage = "Broj telefona mora sadržati 8 ili 9 cifara nakon +381"
        )]
        public string PhoneNumber { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}