using System.Data;
using MySql.Data.MySqlClient;

namespace BugTrackerTest;

public class ControllerTests
{
    [Fact]
    public void ConnectionStringTest()
    {
        const string connectionString = "Server=127.0.0.1;Port=3306;Database=bugtracker;Uid=root;Pwd=test";
        var conection = new MySqlConnection(connectionString);
        conection.Open();
        Assert.Equal(conection.State, ConnectionState.Open);
        conection.Close();
    }
}
