using QuizApp.Business.DTOs;
using QuizApp.WebAPI.Models;

namespace QuizApp.Business.Interfaces
{
    public interface IQuestionService : IBaseService<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync();
    }
}
