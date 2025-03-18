using System.ComponentModel.DataAnnotations;

namespace S18L1.ViewModels
{
    public class EditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }
        [Required]
        public DateOnly BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
