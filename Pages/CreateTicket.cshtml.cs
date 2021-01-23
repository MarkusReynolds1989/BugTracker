using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [BindProperty] [Required] public int WorkerId { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(45)]
        public string _Title { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public void OnPost()
        {
            // Step 1: Authenticate the user. If they are not allowed to see this data we shouldn't 
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                Response.Redirect("Login");
            }

            if (ModelState.IsValid)
            {
                // Step2: Collect the data from the form submission.
                // TODO: Add in try parse to catch any errors.
                WorkerId = int.Parse(Request.Form["WorkerId"]);
                _Title = Request.Form["_Title"];
                Description = Request.Form["Description"];
                var statusIndCd = (StatusIndCd) int.Parse(Request.Form["StatusIndCd"]);
                var loggerId = int.Parse(Request.Form["LoggerId"]);

                // Step 3: Add the ticket to the database. At this point any issues should be caught.
                var ticketController = new TicketController();
                ticketController.Init();
                var ticket = new Models.Ticket(
                    WorkerId
                    , _Title
                    , Description
                    , ""
                    , statusIndCd
                    , loggerId);

                // Step 4: If the ticket is added, we can go over to the other page to show it.
                if (ticketController.Insert(ticket))
                {
                    Response.Redirect("/Tickets");
                }
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