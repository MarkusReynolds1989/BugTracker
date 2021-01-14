using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
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