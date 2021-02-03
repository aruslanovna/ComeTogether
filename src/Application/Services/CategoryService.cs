//using ComeTogether.Domain.Entities;
//using ComeTogether.Infrastructure.Interface;
//using ComeTogether.Service.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ComeTogether.Service.Services
//{
//    public class CategoryService : ICategoryService
//    {
//        public readonly IUnitOfWork _unitOfWork;
//        public CategoryService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task Add(Category e)
//        {
//           await Task.Run(() => _unitOfWork.Categories.Create(e));
//        }

//        public Task Edit(int id, Category e)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<IEnumerable<Category>> GetAll()
//        {
//           return  await Task.Run(() => _unitOfWork.Categories.GetAll());
//        }

//        public async Task<Category> GetById(int id)
//        {
//            return _unitOfWork.Categories.GetById(id);
//        }

//        public async Task<IEnumerable<Category>> GetCategoryByTitle(string search)
//        {
//                var CategoryList = await Task.Run(() => _unitOfWork.Categories.GetByCondition(s => s.CategoryName.Contains(search)).OrderBy(s => s.CategoryName));
//                return CategoryList;
//        }

//        public async Task Remove(int id)
//        {
//           await Task.Run(()=> _unitOfWork.Categories.Delete(id));
//        }
//    }
//}
