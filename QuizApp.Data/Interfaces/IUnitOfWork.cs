using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // DbContext để truy cập trực tiếp nếu cần
        QuizAppDbContext Context { get; }

        // Repository cho các entity
        IGenericRepository<Quiz> QuizRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<UserQuiz> UserQuizRepository { get; }
        IGenericRepository<QuizQuestion> QuizQuestionRepository { get; }
        IGenericRepository<UserAnswer> UserAnswerRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Answer> AnswerRepository { get; }

        // Generic Repository (trả về repo phù hợp với entity)
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

        // Lưu thay đổi vào database
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Transaction Handling
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
