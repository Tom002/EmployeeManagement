using EmployeeManagement.Common.Exceptions;
using EmployeeManagement.Dal.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Dal.Repository
{
    public class LogicService<TEntity> : ILogicService<TEntity>
        where TEntity : class, IEntity
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public LogicService(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id)
                ?? throw new EntityNotFoundException($"Cannot find entity of type {typeof(TEntity).Name} with id={id}");
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return _dbContext.Set<TEntity>().Where(predicate ?? (_ => true)).AsQueryable();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
