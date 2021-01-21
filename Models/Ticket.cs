using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Models
{
    public class Ticket
    {
        [BindProperty]
        public int TicketId { get; }
        [BindProperty]
        public int WorkerId { get; }
        [BindProperty]
        [Required, MaxLength(30)]
        public string Title { get; }
        [BindProperty]
        [Required, MaxLength(200)]
        public string Description { get; }
        [BindProperty]
        [MaxLength(200)]
        public string Resolution { get; }
        [BindProperty]
        [Required, Range(0, 2)]
        public StatusIndCd StatusIndCd { get; }
        [BindProperty]
        [Required]
        public int LoggerId { get; }

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