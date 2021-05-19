using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using ComeTogether.Service.Cast;
using Microsoft.Extensions.Localization;
using WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.SignalR.Hubs;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
     
        public readonly IProjectService _unitOfWork;
        private static readonly IStringLocalizer<BaseController> _localizer;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _context;

        public ProjectsController(UserManager<ApplicationUser> userMgr,IProjectService unitOfWork, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signinMgr, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            userManager = userMgr;
            signInManager = signinMgr;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> NotPartner()
        {
            return View();

                }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                var eventsSearch =  _context.Projects.Where(x => x.ProjectName.StartsWith(searchString)).ToList();
                return View(eventsSearch);
            }
            var events = _context.Projects.ToList();
            return View(events);
        }


        public IActionResult GetAllProjects()
        {
            return View();            
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var events = _context.Projects.ToList();
           
            return DataSourceLoader.Load(events, loadOptions);
        }

        [Authorize]
        public async Task<IActionResult> MyProjects()
        {
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var applicationDbContext = _unitOfWork.GetOrganized(current);
            return View(nameof(Index),  applicationDbContext);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IQueryable<ProjectDetailsModel> project =
   from x in _context.Projects
   where x.ProjectId == id
   select new ProjectDetailsModel
   {
       ProjectId=x.ProjectId,
      Photo  = x.Photo,
      ProjectName=x.ProjectName,
      FullDescription=x.FullDescription,
      ShortDescription=x.ShortDescription,
      StartBudget=x.StartBudget,
      StartDate=x.StartDate,
      RisksAndChallenges=x.RisksAndChallenges,
      Background = x.Background,
       Category = x.Category.CategoryName,
       Founder = x.Founder,
       Partners = _context.Projects
                .Where(user => user.ProjectId == x.ProjectId)
                .Include(user => user.Deals)
                .ThenInclude(userProfiles => userProfiles.Partner).ToArray()
           
        };
            var viewModel = new InstructorIndexData();
            viewModel.Pr = _context.Projects
                .Include(user => user.Deals)
                .ThenInclude(userProfiles => userProfiles.Partner)    
              .ToList();
            Project instructor = viewModel.Pr.Where(
        i => i.ProjectId == id).Single();
            viewModel.App = instructor.Deals.Select(s => s.Partner).ToList();
            ViewBag.App = viewModel.App;




            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }



        public async Task<IActionResult> DetailsForPartner(int id)
        {

            int t = id;
            IQueryable<ProjectDetailsModel> project =
   from x in _context.Projects
   where x.ProjectId == id
   select new ProjectDetailsModel
   {
       ProjectId = x.ProjectId,
       Photo = x.Photo,
       ProjectName = x.ProjectName,
       FullDescription = x.FullDescription,
       ShortDescription = x.ShortDescription,
       StartBudget = x.StartBudget,
       StartDate = x.StartDate,
       RisksAndChallenges = x.RisksAndChallenges,
       Background = x.Background,
       Category = x.Category.CategoryName,
       FounderId = x.FounderId,
       Founder = x.Founder,
       CPO = x.CPO,
       CAC = x.CAC,
       ROMI = x.ROMI,
       ROI = x.ROI,
       ROAS = x.ROAS,
       ARPU = x.ARPU,
       AOV = x.AOV,
       LTV = x.LTV,
       Country=x.Country,
       Partners = _context.Projects
                .Where(user => user.ProjectId == x.ProjectId)
                .Include(user => user.Deals)
                .ThenInclude(userProfiles => userProfiles.Partner).ToArray()

   };
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            ApplicationUser user = await userManager.FindByIdAsync(current);

            var viewModel = new InstructorIndexData();
            viewModel.Pr = _context.Projects
                .Include(user => user.Deals)
                .ThenInclude(userProfiles => userProfiles.Partner)
              .ToList();
            Project instructor = viewModel.Pr.Where(
        i => i.ProjectId == id).Single();
            viewModel.isFounder = _context.Projects.Where(x => x.ProjectId == id).Select(x => x.FounderId).FirstOrDefault() == current;
            viewModel.App = instructor.Deals.Select(s => s.Partner).ToList();
            ViewBag.App = viewModel.App;

           
            if (!viewModel.App.Contains(user) && !viewModel.isFounder) {
                
                return RedirectToAction("NotPartner");
            }

            return View(project);
        }
       
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,FounderId,CategoryId,ShortDescription,FullDescription,RisksAndChallenges,Background,StartBudget,StartDate")] Project project, IFormFile image)
        {
            if (ModelState.IsValid)
            { Project create = project;
                if (image != null && image.Length > 0)

                {
                    create.Photo = ImageConvertor.ConvertImageToBytes(image);
                }
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
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var project =  _unitOfWork.GetById(id);
            var current = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            //if (project.FounderId != current)
            //{
            //    return RedirectToAction("Index");
            //}
            if (project == null)
            {
                return NotFound();
            }

            
            var p = project.Cast<Project>();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", p.CategoryId);
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
    public class InstructorIndexData
    {
        public bool isFounder { get; set; }
        public IEnumerable<Project> Pr { get; set; }
        public IEnumerable<ApplicationUser> App { get; set; }
        public IEnumerable<Deal> De { get; set; }
    }
}
