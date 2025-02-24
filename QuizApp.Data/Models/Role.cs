using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.WebAPI.Models
{
    public class Role : IdentityRole<Guid>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
