using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using BugTracker.Controllers;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
    {
        public void OnGet(int ticketId)
        {
            var ticketController = new TicketController();
            ticketController.Init();
            var ticketList = ticketController.SelectAll();
            // ViewData["list"] = ticketList;
            // Next step pull the ticket we want from the list.
            // Or consume only one item from database.
            ViewData["ticket"] = ticketList.FirstOrDefault(x => x.TicketId == ticketId);
        }
    }
}