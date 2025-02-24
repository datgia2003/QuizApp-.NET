using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Models;

namespace QuizApp.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork.UserRepository)
        {
        }
    }
}
