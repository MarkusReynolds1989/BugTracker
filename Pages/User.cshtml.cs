using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        private IConfiguration _configRoot;

        public User(IConfiguration configRoot)
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
        public string Email { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var userId = int.Parse(Request.Form["UserId"]);
            UserName = Request.Form["UserName"];
            FirstName = Request.Form["FirstName"];
            LastName = Request.Form["LastName"];
            Email = Request.Form["Email"];
            var authLevel = (AuthLevel) int.Parse(Request.Form["AuthLevel"]);

            var userController = new UserController(_configRoot);

            var updateUser = new Models.User(UserName, FirstName, LastName, "", Email, true, authLevel, userId);
            if (await userController.Update(updateUser))
            {
                return new RedirectToPageResult("Users");
            }

            return new RedirectResult($"User?userId={userId}");
        }

        public async Task<IActionResult> OnGet(int userId)
        {
            var loginUserId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (loginUserId == null && authLevel == null || authLevel < 2)
            {
                Response.Redirect("Login?statusCode=401");
            }

            try
            {
                var userController = new UserController(_configRoot);
                // Entry point for the data on the page.
                ViewData["_userId"] = userId;
                return Page();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}