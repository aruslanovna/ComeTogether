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

        public  Project GetById(int id)
        {
            return  _unitOfWork.Projects.GetById(id);
        }
        public IEnumerable<Project> GetAll()
        {
            return _unitOfWork.Projects.GetAll();
        }
        public void AddProject(Project e)
        {
            _unitOfWork.Projects.Create(e);
        }
        public  Project GetByIdWithCategory(int id)
        {
            var project =  _unitOfWork.Projects.GetAllLazyLoad(p => p.ProjectId == id, p => p.Category).AsNoTracking().First();
            return project;
        }

        public void RemoveProject(int id)
        {
            var ProjectForRemoving = _unitOfWork.Projects.GetById(id);
            _unitOfWork.Projects.Delete(ProjectForRemoving);
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

        public IEnumerable<Project> GetProjectByTitle(string search)
        {
            var ProjectList = _unitOfWork.Projects.GetByCondition(s => s.ProjectName.Contains(search)).OrderBy(s => s.ProjectName);
            return ProjectList;
        }

        public IEnumerable<Project> GetProjectByDateStart(string date)
        {
            DateTime searchDate = DateTime.Parse(date);
            var ProjectList = _unitOfWork.Projects.GetByCondition(s => s.StartDate == searchDate).OrderBy(s => s.StartDate);
            return ProjectList;
        }

        public void EditProject(int id, Project e)
        {
            throw new NotImplementedException();
        }

        public Project GetByUserIdWithCategory(string id)
        {
            var project = _unitOfWork.Projects.GetAllLazyLoad(p => p.FounderId == id, p => p.Category).AsNoTracking().First();
            return project;
        }
    }
}
