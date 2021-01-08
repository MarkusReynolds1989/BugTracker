using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Login : PageModel
    {
        public void OnPost()
        {
            var userName = ViewData["UserName"].ToString();
            var password = ViewData["Password"].ToString();
            var login = new LoginController(userName, password);
            if (login != null)
            {
                
            }
        }
    }
}