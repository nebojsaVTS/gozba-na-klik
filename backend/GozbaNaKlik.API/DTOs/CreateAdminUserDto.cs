using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlik.API.DTOs
{
    public class CreateAdminUserDto
    {
        [Required(ErrorMessage = "Username je obavezan.")]
        [MinLength(3, ErrorMessage = "Username mora imati bar 3 karaktera.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Email nije validan.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password je obavezan.")]
        [MinLength(6, ErrorMessage = "Password mora imati bar 6 karaktera.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Rola je obavezna.")]
        public string Role { get; set; } = string.Empty;
    }
}
