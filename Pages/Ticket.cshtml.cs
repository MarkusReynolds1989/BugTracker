using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Controllers;
using BugTracker.Models;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
    {
        public void OnGet(int ticketId)
        {
            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = ticketController.SelectRow(ticketId);
            ViewData["ticket"] = ticket;
        }
        
        // Updates our current value with the values in the fields.
        public void OnPost()
        {
            // Update everything from page.
            // TODO: Clean up parsing the int with error handling, or just check with
            // Typescript. 
            var ticketId = int.Parse(Request.Form["TicketId"]);
            var loggerId = int.Parse(Request.Form["LoggerId"]);
            var workerId = int.Parse(Request.Form["WorkerId"]);
            var statusIndCd = (StatusIndCd) int.Parse(Request.Form["StatusIndCd"]);
            var title = Request.Form["Title"];
            var description = Request.Form["Desc"];
            var resolution = Request.Form["Resolution"];
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

            ticketController.Update(updateTicket);
        }
    }
}