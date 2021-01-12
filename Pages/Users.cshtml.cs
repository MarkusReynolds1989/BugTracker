using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        public void OnGet()
        {
            // Need to store some sort of auth level over in the Login method.
            var userController = new UserController();
            userController.Init();
            var userList = userController.SelectAll();
            ViewData["list"] = userList;
        }
    }
}