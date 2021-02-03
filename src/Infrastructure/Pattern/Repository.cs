using ComeTogether.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComeTogether.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> modelDb;

        private ApplicationDbContext repositoryContext;

        public Repository(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            this.modelDb = repositoryContext.Set<TEntity>();

        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Task.Run(() => modelDb);
        }

        public async Task<IEnumerable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return modelDb.Where(expression);
        }

        public async Task Create(TEntity entity)
        {
            modelDb.Add(entity);
        }

        public async Task Update(TEntity entity)
        {
            modelDb.Attach(entity);
            repositoryContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(TEntity entity)
        {
            if (repositoryContext.Entry(entity).State == EntityState.Detached)
            {
                modelDb.Attach(entity);
            }

            modelDb.Remove(entity);
        }

        public virtual async Task Delete(object id)
        {
            TEntity entityToDelete = modelDb.Find(id);
            Delete(entityToDelete);
        }

        public async Task<TEntity> GetById(object id)
        {
            return modelDb.Find(id);
        }

        public bool Exist(object id)
        {
            var element = GetById(id);
            if (element == null)
                return false;
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = await Include(includeProperties);
            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = await Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        public async Task<IQueryable<TEntity>> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = await Task.Run(() => modelDb.AsNoTracking());
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        public async Task<IQueryable<TEntity>> GetAllLazyLoad(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children)
        {
            children.ToList().ForEach(x => modelDb.Include(x).LoadAsync());
           return  modelDb;
        }
    }
}
