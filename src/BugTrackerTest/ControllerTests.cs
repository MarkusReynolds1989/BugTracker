using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace BugTrackerTest;

public class ControllerTests
{
    [Fact(Skip = "Not yet set up on runner.")]
    public void ConnectionStringTest()
    {
        const string connectionString = "Server=127.0.0.1;Port=3306;Database=bugtracker;Uid=root;Pwd=admin";
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Qodana test.");
        Assert.Equal(ConnectionState.Open, connection.State);
        connection.Close();
    }
}