using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
    {
        [BindProperty]
        [Required]
        [MaxLength(45)]
        public string _Title { get; set; }

        [BindProperty]
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [BindProperty] [MaxLength(300)] public string Resolution { get; set; }

        public IList<SelectListItem> Users { get; set; }

        // Updates our current value with the values in the fields.
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                int.TryParse(Request.Form["TicketId"], out var ticketId);
                int.TryParse(Request.Form["LoggerId"], out var loggerId);
                var workerId = int.Parse(Request.Form["WorkerId"]);
                int.TryParse(Request.Form["StatusIndCd"], out var statusValue);
                var statusIndCd = (StatusIndCd) statusValue;
                _Title = Request.Form["_Title"].ToString();
                Description = Request.Form["Description"].ToString();
                Resolution = Request.Form["Resolution"].ToString();

                var ticketController = new TicketController();
                ticketController.Init();

                var updateTicket =
                    new Models.Ticket(ticketId,
                        workerId,
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

            return Page();
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

            return Page();
        }

        public IActionResult OnGet(int ticketId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
            {
                return new RedirectToPageResult("Login");
            }

            var ticketController = new TicketController();
            ticketController.Init();
            var ticket = ticketController.SelectRow(ticketId);
            if (ticket != null)
            {
                ViewData["Ticket"] = ticket;
                ViewData["_ticketId"] = ticketId;

                // Generate users that can accept tickets.
                var userController = new UserController();
                userController.Init();
                var usersTemp = userController.SelectAll().Where(user => user.AuthLevel != AuthLevel.Guest).ToList();
                Users = usersTemp.Select(user => new SelectListItem
                {
                    Value = user.UserId.ToString(),
                    Text = user.UserName,
                }).ToList();
                ViewData["Users"] = Users;
                Description = ticket.Description;
                Resolution = ticket.Resolution;
                return new PageResult();
            }

            return new RedirectToPageResult("Tickets");
        }
    }
}