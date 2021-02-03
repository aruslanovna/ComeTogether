using System;
using System.Collections.Generic;

    using System.Linq;
    using System.Linq.Expressions;

    using System.Threading.Tasks;

namespace ComeTogether.Infrastructure.Interface
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> expression);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task Delete(object id);

        bool Exist(object id);

        Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IQueryable<TEntity>> Include(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IQueryable<TEntity>> GetAllLazyLoad(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children);

        Task<TEntity> GetById(object id);
    }


}
