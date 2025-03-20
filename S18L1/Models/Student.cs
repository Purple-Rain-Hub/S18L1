using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S18L1.Models
{
    public class Student
    {
        [Key]
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

        public string UserId {  get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
