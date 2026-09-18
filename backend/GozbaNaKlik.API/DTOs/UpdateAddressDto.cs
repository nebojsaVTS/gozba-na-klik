using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class UpdateAddressDto
    {
        [Required(ErrorMessage = "Ulica je obavezna.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Grad je obavezan.")]
        public string City { get; set; } = string.Empty;
    }
}
