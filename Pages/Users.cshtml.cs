using System.Linq;
using System.Threading.Tasks;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class Users : PageModel
    {
        private readonly IConfiguration _configRoot;

        public Users(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public async Task<IActionResult> OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2) // TODO: Check auth level.
            {
                return new RedirectResult("Login?statusCode=401");
            }

            var userController = new UserController(_configRoot);
            var users = await userController.GetAllUsers();
            if (users.Any())
            {
                ViewData["Users"] = users;
                return new PageResult();
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}