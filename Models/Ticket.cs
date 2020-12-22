namespace BugTracker.Models
{
    public class Ticket
    {
        public int TicketId { get; private set; }
        public int WorkerId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Resolution { get; private set; }
        public StatusIndCd StatusIndCd { get; private set; }
        public int LoggerId { get; private set; }
    
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