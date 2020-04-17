using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ComeTogether.Infrastructure.Identity;
using static Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal.LoginModel;

using System;
using WebMVC.SignalR;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure;
using ComeTogether.Domain.Entities;

namespace WebMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<ApplicationUser> userMgr, SignInManager<ApplicationUser> signinMgr, ApplicationDbContext context)
        {
            _context = context;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            InputModel login = new InputModel();
            // login.ReturnUrl = returnUrl;
            return View(login);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(InputModel login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser ApplicationUser = await userManager.FindByEmailAsync(login.Email);
        //        if (ApplicationUser != null)
        //        {
        //            await signInManager.SignOutAsync();
        //            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(ApplicationUser, login.Password, false, false);
        //            if (result.Succeeded)
        //                return Redirect(login.ReturnUrl ?? "/");
        //        }
        //        ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
        //    }
        //    return View(login);
        //}

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
        

       

    }
}