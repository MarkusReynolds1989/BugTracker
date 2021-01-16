using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        public void OnGet(int userId)
        {
            var loginUserId = HttpContext.Session.GetInt32("UserId");
            if (loginUserId == null) // TODO: Check auth level.
            {
                Response.Redirect("Login");
            }

            var userController = new UserController();
            userController.Init();
            var user = userController.SelectRow(userId);
            // Entry point for the data on the page.
            if (user != null)
            {
                ViewData["User"] = user;
            }
            else
            {
                // Give feedback on page that the transaction failed.
            }
        }

        /*public void OnPost()
        {
            // This is where we actually update the user.
            // Gather data from page.
            var userId = int.Parse(Request.Form["UserId"]);
            var userName = Request.Form["UserName"];
            var password = Request.Form["Password"];
            // Don't let them change the active ind here,
            // it should be readonly in the page and throw an error
            // if the manipulate the code.
            var activeInd = bool.Parse(Request.Form["ActiveInd"]);
            var userController = new UserController();
            userController.Init();
            var user = new Models.User(userId, userName, password, activeInd);
            if (userController.Update(user))
            {
                Response.Redirect("Users/");
            }
            else
            {
                // Give feedback on page about why we failed.
            }
        }

        public void OnDelete()
        {
            // Simply set the user to inactive.
            var userController = new UserController();
            userController.Init();
            var userId = int.Parse(Request.Form["UserId"]);
            // Delete method goes here from user id.
            // userController.Delete(userId);
        } */
    }
}