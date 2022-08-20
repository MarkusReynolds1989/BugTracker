using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly IConfiguration _configRoot;

        public CreateUserModel(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        [BindProperty, Required, MaxLength(45)]
        public string UserName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string FirstName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string LastName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string Password { get; set; }

        [BindProperty, Required, MaxLength(45), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string Email { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userAuthLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && userAuthLevel == null || userAuthLevel < 2)
            {
                return Redirect("Login?statusCode=401");
            }

            if (ModelState.IsValid)
            {
                FirstName = Request.Form["FirstName"].ToString();
                LastName = Request.Form["LastName"].ToString();
                UserName = Request.Form["UserName"].ToString();
                Password = Request.Form["Password"].ToString();
                Email = Request.Form["Email"].ToString();
                var authLevel = int.Parse(Request.Form["AuthLevel"].ToString());

                var userController = new UserController(_configRoot);

                if (await userController.Insert(new Models.User(UserName, FirstName, LastName, Email, Password, true,
                    (AuthenticationLevel) authLevel, null)))
                {
                    return new RedirectToPageResult("Users");
                }
            }

            return Page();
        }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2)
            {
                return Redirect("Login");
            }

            return Page();
        }
    }
}