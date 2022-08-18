using System;
using System.Data;
using System.Diagnostics;
using BugTracker.Models;
using MySql.Data.MySqlClient;

// Use this library to hash the input password vs the password in the db.
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BugTracker.Controllers
{
    public class LoginController
    {
        // Dependency injection.
        private readonly IConfiguration _configRoot;

        public LoginController(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        // Set a session to a user from the user that is logged in.
        public async Task<User?> AuthorizeUser(string userName, string password)
        {
            //var hashedPassWord = 
            using var authenticationConnection = new DataConnection(_configRoot);
            using var hash = SHA256.Create();
            var hashedPassword = BitConverter.ToString(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            try
            {
                await using var command = new MySqlCommand
                {
                    CommandType = CommandType.StoredProcedure, Connection = await authenticationConnection.Connect(),
                    CommandText = "AuthenticateUser",
                };
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@ThisPassword", hashedPassword);

                await using var reader = command.ExecuteReader();
                // Build and return user.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new User
                        (
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(5),
                            "", // Don't bring back the hashed password for obvious reasons. No hashing on the client side.
                            reader.GetBoolean(6),
                            (AuthLevel) reader.GetInt32(7),
                            reader.GetInt32(0)
                        );
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}