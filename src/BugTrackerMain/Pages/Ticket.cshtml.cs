using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BugTracker.Controllers;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Pages
{
    public class Ticket : PageModel
    {
        private IConfiguration _configRoot;

        public Ticket(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        [BindProperty, Required, MaxLength(45)]
        public string _Title { get; set; }

        [BindProperty, Required, MaxLength(300)]
        public string Description { get; set; }

        [BindProperty, MaxLength(300)]
        public string Resolution { get; set; }

        public IList<SelectListItem> Users { get; set; }

        // Updates our current value with the values in the fields.
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                int.TryParse(Request.Form["TicketId"], out var ticketId);
                int.TryParse(Request.Form["LoggerId"], out var loggerId);
                var workerId = int.Parse(Request.Form["WorkerId"]);
                int.TryParse(Request.Form["StatusIndCd"], out var statusValue);
                var statusIndCd = (StatusIndicator)statusValue;
                _Title = Request.Form["_Title"].ToString();
                Description = Request.Form["Description"].ToString();
                Resolution = Request.Form["Resolution"].ToString();

                var ticketController = new TicketController(_configRoot);
                var updateTicket = new Models.Ticket(
                    workerId,
                    _Title,
                    Description,
                    Resolution,
                    loggerId,
                    statusIndCd,
                    ticketId
                );

                if (await ticketController.Update(updateTicket))
                {
                    return new RedirectToPageResult("Tickets");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGet(int ticketId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) // TODO: Check auth level.
            {
                return new RedirectToPageResult("Login");
            }

            try
            {
                // Generate the ticket.
                var ticketController = new TicketController(_configRoot);
                var ticket = await ticketController.GetTicket(ticketId);

                ViewData["Ticket"] = ticket;
                ViewData["_ticketId"] = ticketId;

                // Generate users that can accept tickets.
                var userController = new UserController(_configRoot);
                var usersTemp = await userController.GetAllUsers();
                usersTemp = usersTemp
                    .Where(user => user.AuthenticationLevel != AuthenticationLevel.Guest)
                    .ToList();

                Users = usersTemp
                    .Select(
                        user =>
                            new SelectListItem
                            {
                                Value = user.UserId.ToString(),
                                Text = user.UserName,
                            }
                    )
                    .ToList();

                ViewData["Users"] = Users;
                Description = ticket!.Description;
                Resolution = ticket.Resolution!;

                return Page();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return BadRequest();
            }
        }
    }
}
