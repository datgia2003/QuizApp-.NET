using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.WebAPI.Models
{
    public class UserAnswer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserQuizId { get; set; }

        [ForeignKey("UserQuizId")]
        public UserQuiz UserQuiz { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public Guid AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
