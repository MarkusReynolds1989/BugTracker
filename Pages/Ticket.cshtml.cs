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
            ViewData["ticket"] = ticketList.FirstOrDefault(x => x.TicketId == ticketId);
        }
    }
}