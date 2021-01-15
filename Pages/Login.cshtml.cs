using System;
using System.Net;
using BugTracker.Controllers;
using BugTracker.Models;
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
            var user = login.AuthorizeUser(new Models.User(userName, "", "", password, "", AuthLevel.Guest));

            if (user != null && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // Set the users session user id here.
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("UserAuthLevel", (int) user.AuthLevel);
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