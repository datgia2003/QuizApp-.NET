using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.WebAPI.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
