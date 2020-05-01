using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Service.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category e);

        void Edit(int id, Category e);

        void Remove(int id);
        Category GetById(int id);

       

        IEnumerable<Category> GetAll();

        IEnumerable<Category> GetCategoryByTitle(string search);

    



    }
}
