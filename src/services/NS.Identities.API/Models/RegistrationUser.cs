using System.ComponentModel.DataAnnotations;

namespace NS.Identities.API.Models
{
    public class RegistrationUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} field must be between {2} and {1}", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmationPassword { get; set; }
    }
}