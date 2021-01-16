using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Users : PageModel
    {
        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2) // TODO: Check auth level.
            {
                Response.Redirect("Login");
            }

            var userController = new UserController();
            userController.Init();
            var users = userController.SelectAll();
            ViewData["Users"] = users;
        }
    }
}