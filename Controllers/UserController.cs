using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

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
            using var hash = SHA256.Create();
            var hashedPassword = BitConverter.ToString(hash.ComputeHash(Encoding.Unicode.GetBytes(user.Password)));
            try
            {
                using var connection = new DataConnection(_configRoot);
                await using var command = new MySqlCommand("AddUser", await connection.Connect());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Firstname", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@ThisPassword", hashedPassword);
                command.Parameters.AddWithValue("@ThisEmail", user.Email);
                command.Parameters.AddWithValue("AuthLevel", "Admin");
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
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
                success = false;
            }

            return success;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IList<User> users = new List<User>();
            using var connection = new DataConnection(_configRoot);
            await using var command = new MySqlCommand("GetAllUsers", await connection.Connect());
            command.CommandType = CommandType.StoredProcedure;

            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    users.Add(new User(
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(5),
                        "",
                        reader.GetBoolean(6),
                        (AuthLevel) reader.GetInt32(7),
                        reader.GetInt32(0)));
                }
            }

            return users;
        }
    }
}