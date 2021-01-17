using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class CreateUserModel : PageModel
    {
        // Capture user data in this methods data.
        public void OnPost()
        {
            // Step 1: Authenticate the user. If they are not allowed to see this data we shouldn't 
            // let them submit anything, we can control it from here.
            var userId = HttpContext.Session.GetInt32("UserId");
            var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");
            if (userId == null && authLevel == null || authLevel < 2) 
            {
                Response.Redirect("Login?statusCode=401");
            }

            // Step 2: Collect the data from the form, we can safely assume that the JS is going to catch
            // bad data. 
            // TODO: Add name or username and email to the database.  
            var firstName = Request.Form["FirstName"].ToString();
            var lastName = Request.Form["LastName"].ToString();
            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["CreatePassword"].ToString();
            var email = Request.Form["Email"].ToString();

            // Step 3: Call the database to prepare it to consume the data.
            var userController = new UserController();
            userController.Init();

            // Step 4: Submit the data.
            if (userController.Insert(new Models.User(userName, firstName, lastName, password, email, 0)))
            {
                // Step 5: Redirect to the users page to show our added person.
                Response.Redirect("/Users");
            }
            else
            {
                // Post some sort of message that the addition failed.
            }

            // Drawbacks, no mass adding. Has to be one at a time.
            // A little bit slow to redirect.
            // Relying on front end to validate the data, will need to
            // validate it on the back end as well so no one can mess with the client
            // script.
        }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
            {
                Response.Redirect("Login");
            }
        }
    }
}