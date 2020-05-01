using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Interface;
using ComeTogether.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComeTogether.Service.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Category e)
        {
            _unitOfWork.Categories.Create(e);
        }

        public void Edit(int id, Category e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
           return _unitOfWork.Categories.GetAll();
        }

        public Category GetById(int id)
        {
            return _unitOfWork.Categories.GetById(id);
        }

        public IEnumerable<Category> GetCategoryByTitle(string search)
        {
                var CategoryList = _unitOfWork.Categories.GetByCondition(s => s.CategoryName.Contains(search)).OrderBy(s => s.CategoryName);
                return CategoryList;
        }

        public void Remove(int id)
        {
            _unitOfWork.Categories.Delete(id);
        }
    }
}
