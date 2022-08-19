namespace BugTracker.Controllers;

public sealed class DataConnection : IDbConnection
{
    private MySqlConnection Connection { get; set; }
    public string ConnectionString { get; set; }
    public int ConnectionTimeout { get; }
    public string Database { get; }
    public ConnectionState State { get; }

    public DataConnection(
        IConfiguration configRoot,
        MySqlConnection connection,
        string database,
        int connectionTimeout,
        ConnectionState state
    )
    {
        Connection = connection;
        Database = database;
        ConnectionTimeout = connectionTimeout;
        State = state;
        ConnectionString = configRoot.GetValue<string>("ConnectionString:default");
    }

    public async Task<MySqlConnection> Connect()
    {
        Connection = new MySqlConnection { ConnectionString = ConnectionString };
        await Connection.OpenAsync();
        return Connection;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }

    private void Dispose(bool check)
    {
        Connection.Close();
    }

    public IDbTransaction BeginTransaction() => throw new NotImplementedException();

    public IDbTransaction BeginTransaction(IsolationLevel il) =>
        throw new NotImplementedException();

    public void ChangeDatabase(string databaseName)
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        Connection.Close();
    }

    public IDbCommand CreateCommand() => throw new NotImplementedException();

    public void Open()
    {
        Connection = new MySqlConnection { ConnectionString = _connectionString };
        Connection.Open();
    }
}
