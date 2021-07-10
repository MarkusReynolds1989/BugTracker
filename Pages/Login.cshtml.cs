using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Cmp;

namespace BugTracker.Pages
{
    public class Login : PageModel
    {
        private readonly IConfiguration _configRoot;

        private Login(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        [BindProperty, Required, MaxLength(45)]
        public string UserName { get; }

        [BindProperty, Required, MaxLength(45)]
        public string Password { get; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var login = new LoginController(_configRoot); // Injection

            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["Password"].ToString();
            var user = login.AuthorizeUser(userName, password);
            // Set the users session user id here.s
            if (user.UserId.HasValue)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId.Value);
            }

            HttpContext.Session.SetInt32("UserAuthLevel", (int) user.AuthLevel);
            return new RedirectToPageResult("Tickets");

            // Redirect to OnGet(with feedback);
        }

        public void OnGet()
        {
        }
    }
}