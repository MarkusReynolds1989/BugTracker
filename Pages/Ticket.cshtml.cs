using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
    {
        public void OnGet(int ticketId)
        {
            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = ticketController.SelectRow(ticketId);
            if (ticket != null)
            {
                ViewData["Ticket"] = ticket;
            }
            else
            {
                Response.Redirect("https://localhost:5001/Error");
            }
        }

        // Updates our current value with the values in the fields.
        public void OnPost()
        {
            // Update everything from page.
            // TODO: Clean up parsing the int with error handling, or just check with
            // Typescript. 

            // Immutable
            int.TryParse(Request.Form["TicketId"], out var ticketId);
            int.TryParse(Request.Form["LoggerId"], out var loggerId);
            // Worker ID can change.
            int.TryParse(Request.Form["WorkerId"], out var workerId);
            int.TryParse(Request.Form["StatusIndCd"], out var statusValue);
            var statusIndCd = (StatusIndCd) statusValue;
            var title = Request.Form["Title"].ToString();
            var description = Request.Form["Desc"].ToString();
            var resolution = Request.Form["Resolution"].ToString();

            var ticketController = new TicketController();
            ticketController.Init();
            var updateTicket =
                new Models.Ticket(ticketId,
                    workerId,
                    title,
                    description,
                    resolution,
                    statusIndCd,
                    loggerId);
            
            if (ticketController.Update(updateTicket))
            {
                Response.Redirect($"/Tickets");
            }
            else
            {
                Response.Redirect($"/Tickets?ticketId={ticketId}");
            }
        }
    }
}