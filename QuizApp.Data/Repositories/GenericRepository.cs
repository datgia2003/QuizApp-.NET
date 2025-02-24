using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Interfaces;
using QuizApp.WebAPI.Data;
using System.Linq.Expressions;

namespace QuizApp.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly QuizAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(QuizAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T? GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetQuery()
        {
            return _dbSet;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                 string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbSet.FindAsync(id) != null;
        }
    }
}
