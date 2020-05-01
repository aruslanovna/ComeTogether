using ComeTogether.Domain.Entities;
using ComeTogether.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Service.Services
{
    public class ArticleService : IArticleService
    {
        public void Add(Article e)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, Article e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAll()
        {
            throw new NotImplementedException();
        }

        public Article GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Article GetByIdWithCategory(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetArticleByDateStart(string date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetArticleByTitle(string search)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
