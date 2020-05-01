using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNet.SignalR;
using ComeTogether.Service;

namespace WebMVC.Controllers
{
   
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IProjectService _unitOfWork;
        public ProjectsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IProjectService unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
             _unitOfWork = unitOfWork;
        }


        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = _unitOfWork.GetAll();   
            return View( projects.ToList());
        }


        [Authorize]
        public async Task<IActionResult> MyProjects()
        {
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var applicationDbContext = _unitOfWork.GetByUserIdWithCategory(current);
            return View(nameof(Index),  applicationDbContext);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = _unitOfWork.GetByIdWithCategory(id);




            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        [Authorize]
        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,FounderId,CategoryId,ShortDescription,FullDescription,RisksAndChallenges,Background,StartBudget,StartDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                Project create = project;
                create.FounderId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                _unitOfWork.AddProject(create);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", project.CategoryId);
            return View(project);
        }
        [Authorize]
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var project =  _unitOfWork.GetById(id);
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            if (project.FounderId != current)
            {
                return RedirectToAction("Index");
            }
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", project.CategoryId);
            return View(project);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,FounderId,CategoryId,ShortDescription,FullDescription,RisksAndChallenges,Background,StartBudget,StartDate")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            if (project.FounderId != current)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", project.CategoryId);
            return View(project);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project =  _unitOfWork.GetByIdWithCategory(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            _unitOfWork.RemoveProject(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
