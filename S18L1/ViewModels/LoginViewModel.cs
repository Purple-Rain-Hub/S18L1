using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace S18L1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [PasswordPropertyText]
        [Required]
        public required string Password { get; set; }
    }
}
