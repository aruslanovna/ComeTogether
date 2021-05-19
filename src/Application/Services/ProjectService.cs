using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeTogether.Service
{
    public class ProjectService : IProjectService
    {
        public readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Project GetById(int id)
        {
            if (id != null)
            {
                var eventList =  _unitOfWork.ProjectsRepository.GetById(id);
                return eventList;
            }
            return null;
        }
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await Task.Run(() => _unitOfWork.ProjectsRepository.GetAll());
        }
        public void AddProject(Project e)
        {
            _unitOfWork.ProjectsRepository.Create(e);
            _unitOfWork.Save();
        }
        public Project GetByIdWithCategory(int id)
        {
            var project =  _unitOfWork.ProjectsRepository.GetById(id);
           
            return project;
        }

        public async Task RemoveProject(int id)
        {
            var ProjectForRemoving = _unitOfWork.ProjectsRepository.GetById(id);
            _unitOfWork.ProjectsRepository.Delete(ProjectForRemoving);
        }

        //public void EditProject(int id, Project e)
        //{
        //    //var ProjectForEditing = _unitOfWork.Projects.GetByID(id);
        //    //ProjectForEditing.ProjectName = e.ProjectName;
        //    //ProjectForEditing.Background = e.Background;
        //    //ProjectForEditing.CategoryId = e.CategoryId;
        //    //ProjectForEditing.FounderId = e.FounderId;
        //    //ProjectForEditing.RisksAndChallenges = e.RisksAndChallenges;
        //    //ProjectForEditing.ShortDescription = e.ShortDescription;
        //    //ProjectForEditing.StartBudget = e.StartBudget;
        //    //ProjectForEditing.StartDate = e.StartDate;
        //    //ProjectForEditing.FullDescription = e.FullDescription;

        //    _unitOfWork.Projects.Update(ProjectForEditing);
        //}

        //public IEnumerable<Project> GetProjectBy(int id)
        //{
        //    var ProjectList = _unitOfWork.Projects.GetByCondition(x => x.LocationId == id);
        //    return ProjectList;
        //}

        public async Task<IEnumerable<Project>> GetProjectByTitle(string search)
        {
            var projectList = await Task.Run(() =>  _unitOfWork.ProjectsRepository.GetByCondition(s => s.ProjectName.Contains(search)));

            return projectList.OrderBy(s => s.StartDate).ToList();
        }

        public async Task<IEnumerable<Project>> GetProjectByDateStart(string date)
        {
            DateTime searchDate = DateTime.Parse(date);
            //var ProjectList = _unitOfWork.ProjectsRepository.GetByCondition(s => s.StartDate == searchDate).OrderBy(s => s.StartDate);
            //return ProjectList;

            var eventList = await Task.Run(() => _unitOfWork.ProjectsRepository.GetByCondition(s => s.StartDate == searchDate));
            return eventList.OrderBy(s => s.StartDate).ToList();
        }

        public async Task EditProject(int id, Project e)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetOrganized(string id)
        {
            var project = await Task.Run(() => _unitOfWork.ProjectsRepository.GetByCondition(p => p.FounderId == id));
            return project;
        }
    }
}
