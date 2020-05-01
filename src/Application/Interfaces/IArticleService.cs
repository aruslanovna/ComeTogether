using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Service.Interfaces
{
   public interface IArticleService
    {
       
            void Add(Article e);

            void Edit(int id, Article e);

            void Remove(int id);
        Article GetById(int id);

        Article GetByIdWithCategory(int id);


            IEnumerable<Article> GetAll();

            IEnumerable<Article> GetArticleByTitle(string search);

            IEnumerable<Article> GetArticleByDateStart(string date);



        
    }
}
