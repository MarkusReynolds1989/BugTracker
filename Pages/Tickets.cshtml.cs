using System;
using System.Threading.Tasks;
using BugTracker.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class Tickets : PageModel
    {
        private IConfiguration _configRoot;

        public Tickets(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public async Task<IActionResult> OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return new RedirectToPageResult("Login");
            }

            var ticketController = new TicketController(_configRoot);
            var tickets = await ticketController.GetTicketsByWorkerId((int) userId);

            ViewData["Tickets"] = tickets;

            return Page();
        }
    }
}