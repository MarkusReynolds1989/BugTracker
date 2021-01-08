using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class CreateUserModel : PageModel
    {
        public void OnPost()
        {
            // Step 1: Authenticate the user. If they are not allowed to see this data we shouldn't 
            // let them submit anything, we can control it from here.

            // Step 2: Collect the data from the form, we can safely assume that the JS is going to catch
            // bad data. 
            // TODO: Add name or username and email to the database.  
            var name = $"{Request.Form["FirstName"]} {Request.Form["LastName"]}";
            var userName = Request.Form["UserName"].ToString();
            var password = Request.Form["CreatePassword"].ToString();
            var email = Request.Form["Email"].ToString();

            // Step 3: Call the database to prepare it to consume the data.
            var userController = new UserController();
            userController.Init();

            // Step 4: Submit the data.
            userController.Insert(new Models.User(userName, password));

            // Step 5: Redirect to the users page to show our added person.
            Response.Redirect("/Users");

            // Drawbacks, no mass adding. Has to be one at a time.
            // A little bit slow to redirect.
            // Relying on front end to validate the data, will need to
            // validate it on the back end as well so no one can mess with the client
            // script.
        }
    }
}