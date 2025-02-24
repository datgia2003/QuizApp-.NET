using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;

namespace QuizApp.Data.Repositories
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(QuizAppDbContext context) : base(context) { }

        public async Task<IEnumerable<Quiz>> GetQuizzesWithQuestionsAsync()
        {
            return await _context.Quizzes
                .Include(q => q.QuizQuestions)
                .ThenInclude(q => q.Question)
                .ToListAsync();
        }
    }
}
