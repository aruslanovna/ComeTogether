using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure.Interface;

namespace ComeTogether.Infrastructure
{

    public class ProjectRepository : IRepository<Project>
    {
        private ApplicationDbContext db;

        public ProjectRepository( ApplicationDbContext context)
        {
            this.db = context;
        }

            public IEnumerable<Project> GetAll()
            {
                return db.Projects.Include(o => o.Category);
            }

            public Project Get(int? id)
            {
                return db.Projects.Find(id);
            }

            public void Create(Project order)
            {
                db.Projects.Add(order);
            }

            public void Update(Project order)
            {
                db.Entry(order).State = EntityState.Modified;
            }

            public void Delete(int id)
            {
                Project order = db.Projects.Find(id);
                if (order != null)
                    db.Projects.Remove(order);
            }
        
    }
}