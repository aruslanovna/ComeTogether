using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Article> ArticlesRepository { get; }
        IRepository<Project> ProjectsRepository { get; }
        IRepository<Deal> DealsRepository { get; }
        IRepository<Category> CategoriesRepository { get; }
        void Save();
        void SaveChangesAsync();
    }
}
