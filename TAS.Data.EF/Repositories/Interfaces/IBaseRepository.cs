using System.Linq.Expressions;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task AddAsync(T entity);
        void Delete(T Entity);
        Task<IEnumerable<T>> FindMultiple(Expression<Func<T, bool>> predicate);
        Task<T> FindObject(Expression<Func<T, bool>> predicate);
        Task<int> Count();
    }
}
