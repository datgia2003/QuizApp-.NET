using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.WebAPI.Models
{
    public class Question
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(5)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string QuestionType { get; set; } = string.Empty;
        public Guid QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    }
}
