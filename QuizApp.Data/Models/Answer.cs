using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.WebAPI.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(255, MinimumLength = 5)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public bool IsCorrect { get; set; } = false;
        public Guid QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
