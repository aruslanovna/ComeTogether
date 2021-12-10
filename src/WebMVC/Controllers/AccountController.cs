using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal.LoginModel;
using System.Collections.Generic;
using ComeTogether.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Linq;
using WebMVC.Models;
using Microsoft.Extensions.Localization;
using ComeTogether.Domain.Entities;

namespace WebMVC.Controllers
{
  [Authorize]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _context;
        private static readonly IStringLocalizer<BaseController> _localizer;
        public AccountController(UserManager<ApplicationUser> userMgr, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signinMgr, ApplicationDbContext context) 
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public IActionResult Index()
        {
            return View();
        }
      
     
        public async Task<IActionResult> Account()
        {
            var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();

            ApplicationUser user = await userManager.FindByIdAsync(current);

            IEnumerable<UserProfileModel> stores =
                  _context.Articles.Where(x => x.UserId == user.Id).Select(x =>
                 new UserProfileModel
                 {
                     Photo = x.Photo,
                     Briefly = x.Briefly,
                     DatePost = x.PostDate,

                     ArticleId = x.ArticleId,
                     ArticleName = x.Name,
                     Description = x.Description,

                     Comment = _context.Comments
                     .Where(y => y.ArticleId == x.ArticleId)
                     .Select(y => y.Content).ToList(),


                 });

            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.Photo = user.Photo;

            ViewBag.WorkPLace = user.WorkPlace;
            ViewBag.Position = user.Position;
            ViewBag.PhoneNumber = user.PhoneNumber;

            ViewBag.Info = user.Info;
            return View(stores);

        }
        

        public async Task<ActionResult> AnotherUserProfile(string error = "")
        {
            if (error != null)
            {
                ViewBag.Error = error;
            }
            var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();

            IEnumerable<ApplicationUser> members
                = _context.Users.Where(x=>x.Id != current);

            return View(members);

        }

        public async Task<ActionResult> AnotherOneUserProfile(string id, string error)
        {
            var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();

            ApplicationUser user = await userManager.FindByIdAsync(current);
         
            ApplicationUser targetuser =_context.Users.Where(x=>x.Id == id).FirstOrDefault();
           
            bool following = false;

            if (_context.Followers.Any(x => x.FollowerId == user.Id && x.UserId == id))
            {
                following = true;
            }
            ViewBag.Error = error;
            ViewBag.UserName = targetuser.UserName;
            ViewBag.Email = targetuser.Email;
            ViewBag.Photo = targetuser.Photo;           
            ViewBag.WorkPLace = targetuser.WorkPlace;
            ViewBag.Position = targetuser.Position;
            ViewBag.PhoneNumber = targetuser.PhoneNumber;           
            ViewBag.Info = targetuser.Info;
            ViewBag.Following = following;
            ViewBag.Id = targetuser.Id;
            IEnumerable<UserProfileModel> model =
      from x in _context.Articles
      where x.UserId == targetuser.Id
      select new UserProfileModel
      {
          Photo = x.Photo,
          Briefly = x.Briefly,
          DatePost = x.PostDate,

          ArticleId = x.ArticleId,
          ArticleName = x.Name,
          Description = x.Description,
          Topic = x.Category.CategoryName,
          Comment = _context.Comments
                    .Where(y => y.ArticleId == x.ArticleId)
                    .Select(y => y.Content).ToList(),
      };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = signInManager.GetExternalAuthenticationSchemesAsync().Result.ToList()
            };

            //InputModel login = new InputModel();
            // login.ReturnUrl = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback",
                "Account",
                new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

       

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value

                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, false);
                        return View(userInfo);
                    }
                }
                return AccessDenied();
            }
        }


        public async Task<IActionResult> Profile()
        {
            var id = userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(id))
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                return View(user);
            }
            return NotFound();

        }

    }
   
}