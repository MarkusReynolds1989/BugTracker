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

            _ = await connection.ExecuteAsync(
                @"
                insert into ticket(WorkerId, Title, Description, LoggerId)
                values (@WorkerId, @Title, @Description, @LoggerId)",
                new
                {
                    ticket.WorkerId,
                    Title = new DbString
                    {
                        Value = ticket.Title,
                        IsFixedLength = true,
                        Length = 45,
                        IsAnsi = false
                    },
                    Description = new DbString
                    {
                        Value = ticket.Description,
                        IsFixedLength = false,
                        Length = 300,
                        IsAnsi = false
                    },
                    ticket.LoggerId
                }
            );

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
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            _ = await connection.ExecuteAsync(
                @"
                update ticket
                set WorkerId = @WorkerId,
                    Title = @Title,
                    Description = @Description,
                    Resolution = @Resolution,
                    StatusIndicator = @StatusIndicator
                where TicketId = @TicketId
                ",
                new
                {
                    ticket.WorkerId,
                    Title = new DbString
                    {
                        Value = ticket.Title,
                        IsFixedLength = true,
                        Length = 45,
                        IsAnsi = false
                    },
                    Description = new DbString
                    {
                        Value = ticket.Description,
                        IsFixedLength = false,
                        Length = 300,
                        IsAnsi = false
                    },
                    Resolution = new DbString
                    {
                        Value = ticket.Resolution,
                        IsFixedLength = true,
                        Length = 300,
                        IsAnsi = false
                    },
                    ticket.StatusIndicator,
                    ticket.TicketId
                }
            );

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
        try
        {
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            var result = await connection.QueryAsync<Ticket>(
                "select * from ticket where TicketId = @TicketId",
                new { TicketId = ticketId }
            );

            return result.First();
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
        }

        return null;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByWorkerId(int workerId)
    {
        try
        {
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            return await connection.QueryAsync<Ticket>(
                "select * from ticket where WorkerId = @WorkerId",
                new { WorkerId = workerId }
            );
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
        }

        return null;
    }
}
