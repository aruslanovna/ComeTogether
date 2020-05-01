using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Service
{
   public interface IProjectService
    {
        void AddProject(Project e);

        void EditProject(int id, Project e);

        void RemoveProject(int id);
         Project GetById(int id);

        Project GetByIdWithCategory(int id);

        Project GetByUserIdWithCategory(string id);
        IEnumerable<Project> GetAll();

        IEnumerable<Project> GetProjectByTitle(string search);

        IEnumerable<Project> GetProjectByDateStart(string date);

       

    }
}
