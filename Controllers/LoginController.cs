using System;
using System.Data;
using BugTracker.Models;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    // Set a session to a user from the user that is logged in.
    public class LoginController 
    {
        // In this case, this will be created from a post method to
        // local host.
        private readonly MySqlConnectionStringBuilder _authenticationString;

        public LoginController()
        {
            _authenticationString =
                new MySqlConnectionStringBuilder
                {
                    UserID = "admin", Password = "password123", Database = "bug_tracker", Server = ""
                };
        }

        // Return the user from the database if the password and username match.
        // Return null if no match or error.
        public User AuthorizeUser(User user)
        {
            User authenticatedUser = null;
            using var authenticationConnection =
                new MySqlConnection(_authenticationString.ConnectionString);

            try
            {
                authenticationConnection.Open();
                var query =
                    $"SELECT * FROM User WHERE user_name = \"{user.UserName}\" AND password = \"{user.Password}\"";
                var command = authenticationConnection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                using var inputStream = command.ExecuteReader();
                while (inputStream.Read())
                {
                    var userId = inputStream.GetInt32(0);
                    var userName = inputStream.GetString(1);
                    var firstName = inputStream.GetString(2);
                    var lastName = inputStream.GetString(3);
                    var userPassword = inputStream.GetString(4);
                    var email = inputStream.GetString(5);
                    var activeInd = inputStream.GetBoolean(6);
                    var authLevel = inputStream.GetInt32(7);
                    authenticatedUser = new User(userId,
                        userName,
                        firstName,
                        lastName,
                        userPassword,
                        email,
                        activeInd,
                        (AuthLevel) authLevel);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }

            // On false we can write logic to the front end like
            // incorrect username and password.
            // Otherwise, login successful go to next page.
            // and set the user variable to this.
            return authenticatedUser;
        }
    }
}