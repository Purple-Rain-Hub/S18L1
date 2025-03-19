using System.ComponentModel.DataAnnotations;

namespace S18L1.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required DateOnly BirthDate { get; set; }
        [Required]
        public required string Password { get; set; }
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
}
