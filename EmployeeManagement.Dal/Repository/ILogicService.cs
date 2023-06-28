using EmployeeManagement.Dal.Entities.Base;
using System.Linq.Expressions;

namespace EmployeeManagement.Dal.Repository
{
    public interface ILogicService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByIdAsync(long id);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
