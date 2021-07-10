using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class DataConnection : IDisposable

    {
        private readonly IConfiguration _configRoot;
        private readonly string _connectionString;
        public MySqlConnection Connection;

        public DataConnection(IConfiguration configRoot)
        {
            _configRoot = configRoot;
            _connectionString = _configRoot["default"];
        }

        public async Task<MySqlConnection> Connect()
        {
            Connection.ConnectionString = _connectionString;
            await Connection.OpenAsync();
            return Connection;
        }

        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}