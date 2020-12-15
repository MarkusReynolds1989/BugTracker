namespace BugTracker.Models
{
    public class Ticket
    {
        private int TicketId { get; set; }
        private int WorkerId { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private string Resolution { get; set; }
        private StatusIndCd StatusIndCd { get; set; }
        private int LoggerId { get; set; }

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
    }
}