using System.Net;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Login : PageModel
    {
        public void OnPost()
        {
            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["Password"].ToString();
            var login = new LoginController();
            var user = login.AuthorizeUser(new Models.User(0, userName, password, true));
            if (user != null)
            {
                // Set the users session user id here.
                HttpContext.Session.SetInt32("UserId", user.UserId);
                Response.Redirect("Tickets/");
            }
            else
            {
                // No session info, tell the user they can't log in.
                Response.Redirect("Login?statusCode=401");
            }
        }

        public void OnGet(int statusCode = 0)
        {
            if (statusCode == 401)
            {
                ViewData["Feedback"] = "Incorrect username or password.";
            }
        }
    }
}