namespace BugTracker.Controllers;

public sealed class DataConnection : IDbConnection
{
    private MySqlConnection? Connection { get; set; }

    public string? ConnectionString { get; set; }

    public int ConnectionTimeout { get; }
    public string Database { get; } = string.Empty;
    public ConnectionState State { get; } = ConnectionState.Closed;

    public DataConnection(IConfiguration configRoot, int connectionTimeout)
    {
        ConnectionTimeout = connectionTimeout;
        ConnectionString = configRoot.GetValue<string>("ConnectionString:default");
    }

    public async Task<MySqlConnection> Connect()
    {
        Connection = new MySqlConnection { ConnectionString = ConnectionString };
        await Connection.OpenAsync();
        return Connection;
    }

    public IDbCommand CreateCommand()
    {
        throw new NotImplementedException();
    }

    public IDbTransaction BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public IDbTransaction BeginTransaction(IsolationLevel il)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    private void Dispose(bool check)
    {
        if (check)
        {
            Connection?.Close();
        }
    }

    ~DataConnection()
    {
        Dispose();
    }

    // Most likely will not implement.
    public void ChangeDatabase(string databaseName)
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        Connection?.Close();
    }

    public void Open()
    {
        Connection = new MySqlConnection { ConnectionString = ConnectionString };
        Connection.Open();
    }
}
