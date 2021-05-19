
using System;
using System.Threading.Tasks;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Interface;

using ComeTogether.Infrastructure.Repository;

namespace ComeTogether.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        private Repository<Deal> _dealsRepository;
        private Repository<Article> _artRepository;

        private Repository<Project> _projectRepository;

        private Repository<Category> _categoriesRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        public IRepository<Deal> DealsRepository
        {
            get
            {
                return _dealsRepository ??
                   (_dealsRepository = new Repository<Deal>(db));
            }
        }

        public IRepository<Category> CategoriesRepository
        {
            get
            {
                return _categoriesRepository ??
                    (_categoriesRepository = new Repository<Category>(db));
            }
        }
        public IRepository<Project> ProjectsRepository
        {
            get
            {
                return _projectRepository ??
                    (_projectRepository = new Repository<Project>(db));
            }
        }

        public IRepository<Article> ArticlesRepository
        {
            get
            {
                return _artRepository ??
                   (_artRepository = new Repository<Article>(db));
            }
        }

     

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChangesAsync()
        {
            db.SaveChangesAsync();
        }
    }
}
