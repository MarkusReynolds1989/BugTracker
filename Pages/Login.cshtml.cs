using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class Login : PageModel
    {
        private readonly IConfiguration _configRoot;

        public Login(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        [BindProperty, Required, MaxLength(45)]
        public string UserName { get; }

        [BindProperty, Required, MaxLength(45)]
        public string Password { get; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var login = new LoginController(_configRoot); // Injection

            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["Password"].ToString();
            var user = await login.AuthorizeUser(userName, password);
            // Set the users session user id here.
            if (user != null)
            {
                if (user.UserId != null) HttpContext.Session.SetInt32("UserId", (int) user.UserId);
            }
            else
            {
                return RedirectToPage("Login");
            }

            HttpContext.Session.SetInt32("UserAuthLevel", (int) user.AuthLevel);
            return new RedirectToPageResult("Tickets");
        }
    }
}