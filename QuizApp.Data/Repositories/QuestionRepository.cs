using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;

namespace QuizApp.Data.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuizAppDbContext context) : base(context) { }

        public async Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync()
        {
            return await _context.Questiones
                .Include(q => q.Answers)
                .ToListAsync();
        }
    }
}
