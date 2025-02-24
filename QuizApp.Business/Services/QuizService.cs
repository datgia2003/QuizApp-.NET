using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Business.Services
{
    public class QuizService : BaseService<Quiz>, IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork) : base(unitOfWork.QuizRepository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateQuizWithQuestionsAsync(Quiz quiz, List<Question> questions)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.QuizRepository.AddAsync(quiz);
                foreach (var question in questions)
                {
                    question.QuizId = quiz.Id;
                    await _unitOfWork.QuestionRepository.AddAsync(question);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }
        

    }
}
