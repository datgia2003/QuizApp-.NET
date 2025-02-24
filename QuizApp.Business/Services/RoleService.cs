using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Models;

namespace QuizApp.Business.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork.RoleRepository)
        {
        }
    }
}
