using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Infrastructure.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Project> Projects { get; }
        IRepository<Deal> Deals { get; }
        IRepository<Category> Categories { get; }

        Task SaveChangesAsync();
    }
}
