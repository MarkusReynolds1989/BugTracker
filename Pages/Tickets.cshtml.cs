using BugTracker.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Pages
{
    public class Tickets : PageModel
    {
        public void OnGet(int userId)
        {
            // Create a new ticket controller object on the heap.
            var ticketController = new TicketController();
            // Initialize the database connection and string.
            ticketController.Init();
            // Gather all the tickets assigned to the particular userId.
            var tickets = ticketController.SelectAll(userId);
            // You can use this on the page now.
            ViewData["tickets"] = tickets;
        }
    }
}