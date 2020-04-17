using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebMVC.Controllers
{
    public class AdminController : Controller
    {
      
            private UserManager<ApplicationUser> userManager;
            private IPasswordHasher<ApplicationUser> passwordHasher;
            private IPasswordValidator<ApplicationUser> passwordValidator;
            private IUserValidator<ApplicationUser> userValidator;

            public AdminController(UserManager<ApplicationUser> usrMgr, IPasswordHasher<ApplicationUser> passwordHash, IPasswordValidator<ApplicationUser> passwordVal, IUserValidator<ApplicationUser> userValid)
            {
                userManager = usrMgr;
                passwordHasher = passwordHash;
                passwordValidator = passwordVal;
                userValidator = userValid;
            }

            public IActionResult Index()
            {
                return View(userManager.Users);
            }

            public ViewResult Create() => View();

            [HttpPost]
            public async Task<IActionResult> Create(User user)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser ApplicationUser = new ApplicationUser
                    {
                        UserName = user.Name,
                        Email = user.Email,
                       
                    };

                    IdentityResult result = await userManager.CreateAsync(ApplicationUser, user.Password);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
                return View(user);
            }

            public async Task<IActionResult> Update(string id)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                    return View(user);
                else
                    return RedirectToAction("Index");
            }

            [HttpPost]
            public async Task<IActionResult> Update(string id, string email, string password, int age, string country, string salary)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult validEmail = null;
                    if (!string.IsNullOrEmpty(email))
                    {
                        validEmail = await userValidator.ValidateAsync(userManager, user);
                        if (validEmail.Succeeded)
                            user.Email = email;
                        else
                            Errors(validEmail);
                    }
                    else
                        ModelState.AddModelError("", "Email cannot be empty");

                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(password))
                    {
                        validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                        if (validPass.Succeeded)
                            user.PasswordHash = passwordHasher.HashPassword(user, password);
                        else
                            Errors(validPass);
                    }
                    else
                        ModelState.AddModelError("", "Password cannot be empty");

                   

                    //Country myCountry;
                    //Enum.TryParse(country, out myCountry);
                    //user.Country = myCountry;

                   

                    if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded && !string.IsNullOrEmpty(salary))
                    {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Errors(result);
                    }
                }
                else
                    ModelState.AddModelError("", "User Not Found");

                return View(user);
            }

            /*[HttpPost]
            public async Task<IActionResult> Update(string id, string email, string password)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(email))
                        user.Email = email;
                    else
                        ModelState.AddModelError("", "Email cannot be empty");

                    if (!string.IsNullOrEmpty(password))
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    else
                        ModelState.AddModelError("", "Password cannot be empty");

                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                    {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Errors(result);
                    }
                }
                else
                    ModelState.AddModelError("", "User Not Found");
                return View(user);
            }*/

            /*[HttpPost]
            public async Task<IActionResult> Update(string id, string email, string password)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult validEmail = null;
                    if (!string.IsNullOrEmpty(email))
                    {
                        validEmail = await userValidator.ValidateAsync(userManager, user);
                        if (validEmail.Succeeded)
                            user.Email = email;
                        else
                            Errors(validEmail);
                    }
                    else
                        ModelState.AddModelError("", "Email cannot be empty");

                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(password))
                    {
                        validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                        if (validPass.Succeeded)
                            user.PasswordHash = passwordHasher.HashPassword(user, password);
                        else
                            Errors(validPass);
                    }
                    else
                        ModelState.AddModelError("", "Password cannot be empty");

                    if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded)
                    {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Errors(result);
                    }
                }
                else
                    ModelState.AddModelError("", "User Not Found");

                return View(user);
            }*/

            void Errors(IdentityResult result)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            [HttpPost]
            public async Task<IActionResult> Delete(string id)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
                else
                    ModelState.AddModelError("", "User Not Found");
                return View("Index", userManager.Users);
            }
        
    }
}