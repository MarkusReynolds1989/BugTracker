using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class ModifyTicket : PageModel
    {
        public void OnGet(int ticketId)
        {
            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = ticketController.SelectRow(ticketId);
            // Entry point for the data on the page.
            if (ticket != null)
            {
                ViewData["Ticket"] = ticket;
            }
            else
            {
                // Give feedback on page that the transaction failed.
            }
        }

        public void OnPost()
        {
            // Create a ticket object from data on page.
            var ticketId = int.Parse(Request.Form["TicketId"]);
            var workerId = int.Parse(Request.Form["WorkerId"]);
            var title = Request.Form["Title"];
            var description = Request.Form["Description"];
            var resolution = Request.Form["Resolution"];
            // Don't allow them to adjust this to inactive, only active and resolved.
            var statusIndCd = (StatusIndCd) int.Parse(Request.Form["StatusIndCd"]);
            var loggerId = int.Parse(Request.Form["LoggerId"]);
            // Init the ticket object for the update.
            var ticket = new Models.Ticket(
                ticketId
                , workerId
                , title
                , description
                , resolution
                , statusIndCd
                , loggerId);
            // Connection object and logic.
            var ticketController = new TicketController();
            ticketController.Init();
            if (ticketController.Update(ticket))
            {
                Response.Redirect("Tickets/");
            }
            else
            {
                // Message that it didn't work. 
            }
        }

        public void OnDelete(int ticketId)
        {
            var ticketController = new TicketController();
            ticketController.Init();
            //ticketController.Delete(ticketId);
        }
    }
}