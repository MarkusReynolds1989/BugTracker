namespace BugTracker.Models
{
    public class Ticket
    {
        public int TicketId { get; }
        public int WorkerId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Resolution { get; }
        public StatusIndCd StatusIndCd { get; }
        public int LoggerId { get; }
        public bool Active { get; }

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
            Active = true;
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
            Active = true;
        }
    }
}