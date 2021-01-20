using System;
using System.Net;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Login : PageModel
    {
        public IActionResult OnPost()
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
                return new RedirectToPageResult("Tickets");
            }

            Response.Redirect("Login?statusCode=401");
            return new PageResult();
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