using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Service.Interfaces
{
    public interface ICategoryService
    {
        Task Add(Category e);

        Task Edit(int id, Category e);

        Task Remove(int id);
        Task<Category> GetById(int id);

       

        Task<IEnumerable<Category>> GetAll();

        Task<IEnumerable<Category>> GetCategoryByTitle(string search);

    



    }
}
