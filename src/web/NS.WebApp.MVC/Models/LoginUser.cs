using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Models
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} field must be between {2} and {1}", MinimumLength = 8)]
        public string Password { get; set; }
    }
}