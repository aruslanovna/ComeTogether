
using System;
using System.Threading.Tasks;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Interface;
using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure.Repository;

namespace ComeTogether.Infrastructure
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        private IRepository<Deal> _deals;

        private IRepository<Project> _projects;

        private IRepository<Category> _categories;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }
     
        public IRepository<Deal> Deals
        {
            get
            {
                return _deals ??
                   (_deals = new Repository<Deal>(db));
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return _categories ??
                    (_categories = new Repository<Category>(db));
            }
        }
        public IRepository<Project> Projects
        {
            get
            {
                return _projects ??
                    (_projects = new Repository<Project>(db));
            }
        }


        //private ProjectRepository projectRepository;
        //private CategoryRepository categoryRepository;

        //public ProjectRepository Project
        //{
        //    get
        //    {
        //        if (projectRepository == null)
        //            projectRepository = new ProjectRepository(db);
        //        return projectRepository;
        //    }
        //}

        //public CategoryRepository Category
        //{
        //    get
        //    {
        //        if (categoryRepository == null)
        //            categoryRepository = new CategoryRepository(db);
        //        return categoryRepository;
        //    }
        //}



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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
