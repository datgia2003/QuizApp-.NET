using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Models;

namespace QuizApp.Business.Services
{
    public class AnswerService : BaseService<Answer>, IAnswerService
    {
        public AnswerService(IUnitOfWork unitOfWork) : base(unitOfWork.AnswerRepository)
        {
        }
    }
}
