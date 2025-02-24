using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.WebAPI.Models
{
    public class UserQuiz
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
        public string QuizCode { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();

    }
}
