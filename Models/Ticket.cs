using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Models
{
    public class Ticket
    {
        [BindProperty] [Required] public int TicketId { get; }
        [BindProperty] [Required] public int WorkerId { get; }

        [BindProperty]
        [Required, MaxLength(45)]
        public string Title { get; }

        [BindProperty]
        [Required, MaxLength(300)]
        public string Description { get; }

        [BindProperty] [MaxLength(300)] public string Resolution { get; }
        [BindProperty] [Required] public StatusIndCd StatusIndCd { get; }
        [BindProperty] [Required] public int LoggerId { get; }

        // Constructor for ticket that already exists.
        public Ticket(int ticketId, int workerId, string title, string description, string resolution,
            StatusIndCd statusIndCd, int loggerId)
        {
            TicketId = ticketId;
            WorkerId = workerId;
            Title = title;
            Description = description;
            Resolution = resolution;
            StatusIndCd = statusIndCd;
            LoggerId = loggerId;
        }

        // Constructor for creating a new ticket.
        public Ticket(int workerId, string title, string description, string resolution,
            StatusIndCd statusIndCd, int loggerId)
        {
            WorkerId = workerId;
            Title = title;
            Description = description;
            Resolution = resolution;
            StatusIndCd = statusIndCd;
            LoggerId = loggerId;
        }
    }
}