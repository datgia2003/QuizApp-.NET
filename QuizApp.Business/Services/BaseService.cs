using QuizApp.Business.DTOs;
using QuizApp.Business.Interfaces;
using QuizApp.Data.Interfaces;
using System.Linq.Expressions;

namespace QuizApp.Business.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public BaseService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<int> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return 1; // Trả về 1 nếu thêm thành công
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _repository.Update(entity);
            return true;
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return _repository.Delete(id);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _repository.Delete(entity);
            return true;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginatedResult<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _repository.Get(filter, orderBy, includeProperties);

            var totalCount = query.Count();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedResult<T>(items, totalCount, pageIndex, pageSize);
        }
    }
}
