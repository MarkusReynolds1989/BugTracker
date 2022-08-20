namespace BugTracker.Controllers;

public class TicketController
{
    private readonly IConfiguration _configRoot;

    public TicketController(IConfiguration configRoot)
    {
        _configRoot = configRoot;
    }

    public async Task<bool> Insert(Ticket ticket)
    {
        bool success;

        try
        {
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            _ = await connection.ExecuteAsync();
            success = true;
        }
        catch (MySqlException exception)
        {
            success = false;
            Debug.WriteLine(exception);
        }

        return success;
    }

    public async Task<bool> Update(Ticket ticket)
    {
        bool success;

        try
        {
            success = true;
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
            success = false;
        }

        return success;
    }

    public async Task<Ticket?> GetTicket(int ticketId)
    {
        Ticket? ticket = null;
        try { }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
        }

        return ticket;
    }

    /*public async Task<IEnumerable<Ticket>> GetTicketsByWorkerId(int workerId)
    {
        try { }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
        }
        return new[] { };
    }*/
}
