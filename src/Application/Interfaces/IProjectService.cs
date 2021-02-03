using ComeTogether.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComeTogether.Service
{
   public interface IProjectService
    {
        Task AddProject(Project e);

        Task EditProject(int id, Project e);

        Task RemoveProject(int id);
        Task<Project> GetById(int id);

        Task<Project> GetByIdWithCategory(int id);

        Task<IEnumerable<Project>> GetOrganized(string id);
        Task<IEnumerable<Project>> GetAll();

        Task<IEnumerable<Project>> GetProjectByTitle(string search);

        Task<IEnumerable<Project>> GetProjectByDateStart(string date);

       

    }
}
