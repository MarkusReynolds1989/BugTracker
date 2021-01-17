using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
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
            // let them submit anything, w
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
            {
                Response.Redirect("Login");
            }

            // Step2: Collect the data from the form submission.
            // TODO: Add in try parse to catch any errors.
            var workerId = int.Parse(Request.Form["WorkerId"]);
            var title = Request.Form["Title"];
            var description = Request.Form["Description"];
            var statusIndCd = (StatusIndCd) int.Parse(Request.Form["StatusIndCd"]);
            var loggerId = int.Parse(Request.Form["LoggerId"]);

            // Step 3: Add the ticket to the database. At this point any issues should be caught.
            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = new Models.Ticket(
                workerId
                , title
                , description
                , ""
                , statusIndCd
                , loggerId);

            // Step 4: If the ticket is added, we can go over to the other page to show it.
            if (ticketController.Insert(ticket))
            {
                Response.Redirect("/Tickets");
            }
            else
            {
                // Post some sort of message that the addition failed.
            }
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