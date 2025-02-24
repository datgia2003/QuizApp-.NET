using QuizApp.WebAPI.Models;

namespace QuizApp.Data.Interfaces
{
    public interface IQuizRepository : IGenericRepository<Quiz>
    {
        Task<IEnumerable<Quiz>> GetQuizzesWithQuestionsAsync();
    }
}
