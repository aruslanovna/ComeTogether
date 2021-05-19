using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ComeTogether.Infrastructure.Identity;
using Microsoft.Extensions.Localization;
using ComeTogether.Domain.Entities;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ClaimController : Controller
    {


        private UserManager<ApplicationUser> userManager;
        private IAuthorizationService authService;
        private static readonly IStringLocalizer<BaseController> _localizer;
        public ClaimController(UserManager<ApplicationUser> userMgr, IAuthorizationService auth)
        {
            userManager = userMgr;
            authService = auth;
        }

        public ViewResult Index() => View(User?.Claims);

        public ViewResult Create() => View();

        [Authorize(Policy = "AspManager")]
        public ViewResult Project() => View("Index", User?.Claims);

        [Authorize(Policy = "AllowTom")]
        public ViewResult TomFiles() => View("Index", User?.Claims);

        public async Task<IActionResult> PrivateAccess(string title)
        {
            string[] allowedUsers = { "tom", "alice" };
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, allowedUsers, "PrivateAccess");

            if (authorized.Succeeded)
                return View("Index", User?.Claims);
            else
                return new ChallengeResult();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);
            //when claims are added to database like here in ApplicationUser then issuer cannot be changed and is always set as 'LOCAL AUTHORITY'
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await userManager.AddClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            ApplicationUser user = await userManager.GetUserAsync(HttpContext.User);

            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];

            Claim claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();

            IdentityResult result = await userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);

            return View("Index");
        }

        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}