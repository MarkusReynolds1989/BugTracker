namespace BugTracker.Models
{
    public record Ticket(int WorkerId, string Title, string Description, string Resolution, int LoggerId,
        StatusIndCd StatusIndCd = StatusIndCd.Open, int? TicketId = null);
}