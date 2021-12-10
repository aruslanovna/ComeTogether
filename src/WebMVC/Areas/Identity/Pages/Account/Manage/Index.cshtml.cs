using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Application.Common.Interfaces;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using ComeTogether.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationDbContext _context;
        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager; _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string WorkPlace { get; set; }
            public string Position { get; set; }
            public string Info { get; set; }


            public string LastName { get; set; }


            public DateTime? BirthDate { get; set; }

            public string Address { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }

            public string BusinessRegister { get; set; }
            public MainNacel? MainNacel { get; set; }
           
            public ICollection<Deal> Deals { get; set; }
            public ICollection<Nacel> BusinessRegisters { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var workPlace = user.WorkPlace ;
            var lastName = user.LastName;
            var position = user.Position ;
            var address = user.Address ;
            var city = user.City ;
            var postalCode = user.PostalCode ;
            var mainNacel = user.MainNacel;
            var deals = user.Deals ;
            var country = user.Country ;


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                WorkPlace = workPlace,
                Position = position,

                LastName = lastName,

                Address = address,
                City = city,

                PostalCode = postalCode,
                Country = country,
                Deals = deals,
                MainNacel = mainNacel

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            try
            {
                user.PhoneNumber = Input.PhoneNumber;
                user.City = Input.City;
                user.WorkPlace = Input.WorkPlace;
                user.Position = Input.Position;
                user.LastName = Input.LastName;
                user.Address = Input.Address;
                user.PostalCode = Input.PostalCode;
                user.Country = Input.Country;
                user.MainNacel = Input.MainNacel;
            }
            catch
            {
               
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting param for user with ID '{userId}'.");
                
            }
               
            

            await _signInManager.RefreshSignInAsync(user);
            _userManager.UpdateAsync(user);
            _context.SaveChanges();
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
