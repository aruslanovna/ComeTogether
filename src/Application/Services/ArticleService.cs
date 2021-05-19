using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Interface;
using ComeTogether.Service.Interfaces;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace ComeTogether.Service.Services
{
    public class ArticleService : IArticleService
    {

        public readonly IUnitOfWork _unitOfWork;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Article e)
        {
             _unitOfWork.ArticlesRepository.Create(e);
            _unitOfWork.Save();
        }

        public async Task Edit(int id, Article e)
        {
            Article edit =  GetById(id);
            edit = e;
            _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var eventList = await _unitOfWork.ArticlesRepository.GetAll();
            return eventList.OrderBy(s => s.Name).ToList();
        }

        public Article GetById(int? id)
        {
            if (id != null)
            {
                var eventList = _unitOfWork.ArticlesRepository.GetById(id);
                return eventList;
            }
            return null;
        }

        public async Task<IEnumerable<Article>> GetByIdWithCategory(int id)
        {
            return await Task.Run(() => _unitOfWork.ArticlesRepository.GetWithInclude(x => x.Category));
        }

        public async Task<IEnumerable<Article>> GetArticleByDateStart(string date)
        {
            return await Task.Run(() => _unitOfWork.ArticlesRepository.GetByCondition(x => x.PostDate == DateTime.Parse(date)));
        }
        public async Task<IEnumerable<Article>> GetArticleByDateStartAndLater(string date)
        {
            return await Task.Run(() => _unitOfWork.ArticlesRepository.GetByCondition(x => x.PostDate <= DateTime.Parse(date)));
        }

        public async Task<IEnumerable<Article>> GetArticleByDateStartAndearlier(string date)
        {
            return await Task.Run(() => _unitOfWork.ArticlesRepository.GetByCondition(x => x.PostDate >= DateTime.Parse(date)));
        }
        public IEnumerable<Article> GetArticleByTitle(string search)
        {
            var eventList = _unitOfWork.ArticlesRepository.GetByCondition(s => s.Name.StartsWith(search));
            return eventList.OrderBy(s => s.Name).ToList();
        }


        public async Task Remove(int? id)
        {
            if (id != null)
            {
                var eventList = await Task.Run(() => _unitOfWork.ArticlesRepository.GetById(id));
                _unitOfWork.ArticlesRepository.Delete(id);
                _unitOfWork.SaveChangesAsync();

            }

        }


        public async Task<bool> Exist(int? id)
        {
            return await Task.Run(() => _unitOfWork.ArticlesRepository.Exist(id));
        }
    }
}
