namespace BugTracker.Models;

// StatusIndCd is set to open by default and in sql.
// TicketId is null in the case where we will create a new one.
public class Ticket
{
    public int              WorkerId        { get; set; }
    public string           Title           { get; set; }
    public string           Description     { get; set; }
    public string           Resolution      { get; set; }
    public int              LoggerId        { get; set; }
    public StatusIndicator? StatusIndicator { get; set; }
    public int              TicketId        { get; set; }

    public Ticket(
        int             workerId,
        string          title,
        string          description,
        string          resolution,
        int             loggerId,
        StatusIndicator statusIndicator,
        int             ticketId
    )
    {
        WorkerId = workerId;
        Title = title;
        Description = description;
        Resolution = resolution;
        LoggerId = loggerId;
        StatusIndicator = statusIndicator;
        TicketId = ticketId;
    }

    public Ticket(
        int    workerId,
        string title,
        string description,
        int    loggerId
    )
    {
        WorkerId = workerId;
        Title = title;
        Description = description;
        LoggerId = loggerId;
    }

    public Ticket()
    {
    }
}