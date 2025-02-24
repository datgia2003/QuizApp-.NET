using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.WebAPI.Models
{
    public class Quiz
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 3600)]
        public int Duration { get; set; }

        [MaxLength(500)]
        public string? ThumbnailUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
        public ICollection<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();
    }
}
