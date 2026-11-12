using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class CreateRestaurantDto
    {
        [Required(ErrorMessage = "Naziv restorana je obavezan.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa je obavezna.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj telefona je obavezan.")]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool SupportsDelivery { get; set; }

        public bool SupportsPickup { get; set; }

        public int OwnerId { get; set; }
    }
}