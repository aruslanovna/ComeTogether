using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Service.Interfaces
{
   public interface IArticleService
    {
       
      void Add(Article e);

        Task Edit(int id, Article e);

        Task Remove(int? id);
        Task<Article> GetById(int? id);

        Task<IEnumerable<Article>> GetByIdWithCategory(int id);


        Task<IEnumerable<Article>> GetAll();

        Task<IEnumerable<Article>> GetArticleByTitle(string search);

        Task<IEnumerable<Article>> GetArticleByDateStart(string date);

        Task<bool> Exist(int? id);


    }
}
