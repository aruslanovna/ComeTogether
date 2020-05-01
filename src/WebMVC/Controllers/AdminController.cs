using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebMVC.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> Back()
        {
            DateTime d = DateTime.Now;
            string dd = d.Day + "-" + d.Month + "-" + d.Hour;

            string aaa = @"Server=DESKTOP-AGJ5FF3\SQLEXPRESS;Initial Catalog=ComeTogetherDB;Persist Security Info=False;User ID=User;Password=User;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=False;";
            SqlConnection con = new SqlConnection(aaa);
            //con.ConnectionString = ConfigurationManager.ConnectionStrings["BackupCatalogDBSoft.Properties.Settings."+dbname+"ConnectionString"].ToString();

            con.Open();
            string str = "USE " + "BranchDB" + ";";
            string str1 = "BACKUP DATABASE " + "ComeTogetherDb" +
                " TO DISK = 'E:\\" + "ComeTogetherDB" + "_" + dd +
                ".Bak' WITH FORMAT,MEDIANAME = 'Z_SQLServerBackups',NAME = 'Full Backup of " + "BranchDB" + "';";
            ViewBag.Path = "E:\\" + "ComeTogetherDB" + "_" + dd + ".Bak";
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlCommand cmd2 = new SqlCommand(str1, con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();

            con.Close();
            return View();
            
        }
        public ActionResult OtherAction()
        {
            return View(GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

            return dict;
        }

    }
}