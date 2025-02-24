using QuizApp.Business.DTOs;
using QuizApp.WebAPI.Models;

namespace QuizApp.Business.Interfaces
{
    public interface IQuizService : IBaseService<Quiz>
    {
        Task<bool> CreateQuizWithQuestionsAsync(Quiz quiz, List<Question> questions);

    }
}
