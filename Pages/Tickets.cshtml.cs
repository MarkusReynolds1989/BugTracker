using System;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Tickets : PageModel
    {
        public void OnGet()
        {
            // If this try fails we don't want them to access this page as they haven't logged in.
            var userId = HttpContext.Session.GetInt32("UserId");
            // Create a new ticket controller object on the heap.
            var ticketController = new TicketController();
            // Initialize the database connection and string.
            ticketController.Init();
            // Gather all the tickets assigned to the particular userId.
            // Have to use this BitConverter here to get the data.
            var tickets = ticketController.SelectAll(userId);
            // You can use this on the page now.
            ViewData["tickets"] = tickets;
        }
    }
}