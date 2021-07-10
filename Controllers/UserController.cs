using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class UserController
    {
        private IConfiguration _configRoot;

        public UserController(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public async Task<bool> Insert(User user)
        {
            bool success;

            try
            {
                using var connection = new DataConnection(_configRoot);
                await using var command = new MySqlCommand("AddUser", await connection.Connect());
                command.CommandType = CommandType.StoredProcedure;
                await command.ExecuteNonQueryAsync();
                success = true;
            }
            catch (MySqlException exception)
            {
                Debug.WriteLine(exception);
                success = false;
            }

            return success;
        }

        public async Task<bool> Update(User user)
        {
            bool success;

            try
            {
                success = true;
            }
            catch (MySqlException exception)
            {
                success = false;
                Debug.WriteLine(exception);
            }

            return success;
        }
    }
}