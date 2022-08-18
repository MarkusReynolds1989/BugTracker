#nullable enable
namespace BugTracker.Models
{
    // StatusIndCd is set to open by default and in sql.
    // TicketId is null in the case where we will create a new one.
    public record Ticket(int WorkerId, string Title, string Description, string? Resolution, int LoggerId,
        StatusIndCd? StatusIndCd, int? TicketId);
}