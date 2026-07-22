using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Email nije validan.")]
        public string Email { get; set; } = string.Empty;

        public string? Password { get; set; }
    }
}