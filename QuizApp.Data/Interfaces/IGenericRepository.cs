using System.Linq.Expressions;

namespace QuizApp.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // 🔹 Lấy tất cả bản ghi
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        // 🔹 Lấy bản ghi theo ID
        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);

        // 🔹 Thêm, cập nhật, xóa
        Task AddAsync(T entity);
        void Update(T entity);
        bool Delete(Guid id); // ✅ Sửa thành bool để tránh lỗi trong BaseService
        void Delete(T entity);

        // 🔹 Truy vấn dữ liệu LINQ
        IQueryable<T> GetQuery();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);

        // 🔹 Tìm kiếm dữ liệu
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // 🔹 Hỗ trợ filter, sort, include (dùng cho LINQ)
        IQueryable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        // 🔹 Kiểm tra bản ghi có tồn tại không
        Task<bool> Exists(Guid id);
    }
}
