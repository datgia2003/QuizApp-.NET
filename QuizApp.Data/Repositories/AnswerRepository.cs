using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;

namespace QuizApp.Data.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>
    {
        public AnswerRepository(QuizAppDbContext context) : base(context) { }
    }
}
