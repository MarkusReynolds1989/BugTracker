using System.ComponentModel.DataAnnotations;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class CreateUserModel : PageModel
    {
        [BindProperty, Required, MaxLength(45)]
        public string UserName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string FirstName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string LastName { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string Password { get; set; }

        [BindProperty, Required, MaxLength(45), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [BindProperty, Required, MaxLength(45)]
        public string Email { get; set; }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2)
            {
                Response.Redirect("Login?statusCode=401");
            }

            if (ModelState.IsValid)
            {
                FirstName = Request.Form["FirstName"].ToString();
                LastName = Request.Form["LastName"].ToString();
                UserName = Request.Form["UserName"].ToString();
                Password = Request.Form["Password"].ToString();
                Email = Request.Form["Email"].ToString();
                var authlevel = int.Parse(Request.Form["AuthLevel"].ToString());

                var userController = new UserController();
                userController.Init();

                if (userController.Insert(new Models.User(UserName, FirstName, LastName, Password, Email,
                    (AuthLevel) authlevel)))
                {
                    return new RedirectToPageResult("Users");
                }
            }

            return Page();
        }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2)
            {
                Response.Redirect("Login");
            }
        }
    }
}