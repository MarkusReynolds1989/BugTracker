using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class DataConnection : IDisposable

    {
        private readonly IConfiguration _configRoot;
        private readonly string _connectionString;
        private MySqlConnection Connection { get; set; }

        public DataConnection(IConfiguration configRoot)
        {
            _configRoot = configRoot;
            _connectionString = _configRoot.GetValue<string>("ConnectionString:default");
        }

        public async Task<MySqlConnection> Connect()
        {
            Connection = new MySqlConnection {ConnectionString = _connectionString};
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