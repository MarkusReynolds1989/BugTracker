using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ubiety.Dns.Core.Records.NotUsed;

namespace BugTracker.Pages
{
    public class User : PageModel
    {
        public void OnGet(int userId)
        {
            var loginUserId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (loginUserId == null && authLevel == null || authLevel < 2)
            {
                Response.Redirect("Login?statusCode=401");
            }

            var userController = new UserController();
            userController.Init();
            var user = userController.SelectRow(userId);
            // Entry point for the data on the page.
            if (user != null)
            {
                ViewData["User"] = user;
                ViewData["_userId"] = userId;
            }
            else
            {
                // Give feedback on page that the transaction failed.
            }
        }

        public IActionResult OnPost()
        {
            // This is where we actually update the user.
            // Gather data from page.
            var userId = int.Parse(Request.Form["UserId"]);
            var userName = Request.Form["UserName"];
            var firstname = Request.Form["FirstName"];
            var lastname = Request.Form["LastName"];
            var email = Request.Form["Email"];
            var authLevel = (AuthLevel) int.Parse(Request.Form["AuthLevel"]);

            var userController = new UserController();
            userController.Init();

            var updateUser = new Models.User(userId, userName, firstname, lastname, "", email, true, authLevel);
            if (userController.Update(updateUser))
            {
                return new RedirectToPageResult("Users");
            }

            return new RedirectResult($"User?userId={userId}");
        }

        public IActionResult OnPostDelete()
        {
            // Simply set the user to inactive.
            var userController = new UserController();
            userController.Init();
            var userId = int.Parse(Request.Form["UserId"]);
            // Delete method goes here from user id.
            if (userController.Delete(userId))
            {
                return new RedirectToPageResult("Users");
            }

            return new RedirectToPageResult($"User?userId={userId}");
        }
    }
}