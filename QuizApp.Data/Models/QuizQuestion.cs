using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.WebAPI.Models
{
    public class QuizQuestion
    {
        [Required]
        public Guid QuizId { get; set; }

        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public int Order { get; set; }
    }
}
