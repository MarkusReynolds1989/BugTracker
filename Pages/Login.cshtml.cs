using System;
using System.Net;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Asn1.Cmp;

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

            return new ChallengeResult();
        }

        public void OnGet()
        {
        }
    }
}