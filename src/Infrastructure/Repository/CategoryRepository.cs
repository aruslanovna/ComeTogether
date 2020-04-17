using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Application.Common.Interfaces;
using System.Collections.Generic;
using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure.Interface;

namespace ComeTogether.Infrastructure.Repository
{
    
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int? id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category book)
        {
            db.Categories.Add(book);
        }

        public void Update(Category book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Category book = db.Categories.Find(id);
            if (book != null)
                db.Categories.Remove(book);
        }

        public bool Any(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}
