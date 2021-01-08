using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class CreateTicketModel : PageModel
    {
        // On completion of creation we can then have the data go to the users list.
        public void OnPost()
        {
           // Step 1: Authenticate the user. If they are not allowed to see this data we shouldn't 
           // let them submit anything, we can control it from here.
           
           // Step 2: Collect the data from the form, we can safely assume that the JS is going to catch
           // bad data.
        }
    }
}
