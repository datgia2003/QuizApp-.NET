using Microsoft.EntityFrameworkCore.Storage;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizAppDbContext _context;
        private IDbContextTransaction? _transaction;

        public QuizAppDbContext Context => _context;

        public IGenericRepository<Quiz> QuizRepository { get; }
        public IGenericRepository<Question> QuestionRepository { get; }
        public IGenericRepository<UserQuiz> UserQuizRepository { get; }
        public IGenericRepository<QuizQuestion> QuizQuestionRepository { get; }
        public IGenericRepository<UserAnswer> UserAnswerRepository { get; }
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<Answer> AnswerRepository { get; }

        public UnitOfWork(QuizAppDbContext context)
        {
            _context = context;
            QuizRepository = new GenericRepository<Quiz>(_context);
            QuestionRepository = new GenericRepository<Question>(_context);
            UserQuizRepository = new GenericRepository<UserQuiz>(_context);
            QuizQuestionRepository = new GenericRepository<QuizQuestion>(_context);
            UserAnswerRepository = new GenericRepository<UserAnswer>(_context);
            UserRepository = new GenericRepository<User>(_context);
            RoleRepository = new GenericRepository<Role>(_context);
            AnswerRepository = new GenericRepository<Answer>(_context);
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
