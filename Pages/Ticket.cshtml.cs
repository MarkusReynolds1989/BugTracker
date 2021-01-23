using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
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

        [BindProperty] [MaxLength(300)] public string Resolution { get; set; }

        // Updates our current value with the values in the fields.
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                int.TryParse(Request.Form["TicketId"], out var ticketId);
                int.TryParse(Request.Form["LoggerId"], out var loggerId);
                WorkerId = int.Parse(Request.Form["WorkerId"]);
                int.TryParse(Request.Form["StatusIndCd"], out var statusValue);
                var statusIndCd = (StatusIndCd) statusValue;
                _Title = Request.Form["Title"].ToString();
                Description = Request.Form["Description"].ToString();
                Resolution = Request.Form["Resolution"].ToString();

                var ticketController = new TicketController();
                ticketController.Init();

                var updateTicket =
                    new Models.Ticket(ticketId,
                        WorkerId,
                        _Title,
                        Description,
                        Resolution,
                        statusIndCd,
                        loggerId);

                if (ticketController.Update(updateTicket))
                {
                    return new RedirectToPageResult("Tickets");
                }
            }

            return new PageResult();
        }

        public IActionResult OnPostDelete()
        {
            // Simply set the user to inactive.
            var ticketController = new TicketController();
            ticketController.Init();
            var ticketId = int.Parse(Request.Form["TicketId"]);
            // Delete method goes here from user id.
            if (ticketController.Delete(ticketId))
            {
                return new RedirectToPageResult("Tickets");
            }

            return new RedirectToPageResult($"Tickets?ticketId={ticketId}");
        }

        public void OnGet(int ticketId = 0)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
            {
                Response.Redirect("Login");
            }

            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = ticketController.SelectRow(ticketId);
            if (ticket != null)
            {
                ViewData["Ticket"] = ticket;
                ViewData["_ticketId"] = ticketId;
            }
            else
            {
                Response.Redirect("https://localhost:5001/Error");
            }
        }
    }
}