using Microsoft.EntityFrameworkCore;
using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Business.Services
{
    public class QuestionService : BaseService<Question>, IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork) : base(unitOfWork.QuestionRepository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync()
        {
            return await _unitOfWork.QuestionRepository.GetAllAsync();
        }
        public async Task<bool> DeleteQuestionAsync(Guid id)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return false;
            }

            // 🔥 Xóa thủ công tất cả QuizQuestion trước khi xóa Question
            var quizQuestions = await _unitOfWork.QuizQuestionRepository.GetQuery(q => q.QuestionId == id).ToListAsync();
            foreach (var qq in quizQuestions)
            {
                _unitOfWork.QuizQuestionRepository.Delete(qq);
            }

            _unitOfWork.QuestionRepository.Delete(question);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
