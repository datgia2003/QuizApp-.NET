using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.WebAPI.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty;

        public string DisplayName => $"{FirstName} {LastName}";

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(500)]
        public string? Avatar { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
