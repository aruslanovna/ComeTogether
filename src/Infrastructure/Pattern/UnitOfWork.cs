
using System;
using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure.Repository;

namespace ComeTogether.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        private ProjectRepository projectRepository;
        private CategoryRepository categoryRepository;

        public ProjectRepository Project
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public CategoryRepository Category
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
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
    }
}
