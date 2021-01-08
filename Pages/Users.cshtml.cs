using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        public void OnGet()
        {
            var userController = new UserController();
            userController.Init();
            var userList = userController.SelectAll();
            ViewData["list"] = userList;
        }
    }
}