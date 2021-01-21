using System;
using System.Collections.Generic;
using System.Data;
using BugTracker.Models;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class UserController : IDataProcess<User>
    {
        public MySqlConnectionStringBuilder AuthenticationString { get; set; }
        public MySqlConnection Authentication { get; set; }

        public bool Init()
        {
            // This is temporary, and when we go to prod we will change this.
            AuthenticationString = new MySqlConnectionStringBuilder
            {
                UserID = "admin", Password = "password123", Database = "bug_tracker",
                Server = "***REMOVED***"
            };
            Authentication = new MySqlConnection(AuthenticationString.ConnectionString);
            // Open the connection.
            Authentication.Open();
            // If we are able to connect then we know our connection worked, otherwise we should close. 
            if (Authentication.State == ConnectionState.Open)
            {
                Authentication.Close();
                return true;
            }

            Authentication.Close();
            return false;
        }

        public bool Insert(User user)
        {
            var query = "INSERT INTO User (user_name, first_name," +
                        "last_name, password, email, auth_level) " +
                        $"VALUES (\"{user.UserName}\", \"{user.Firstname}\"," +
                        $"\"{user.Lastname}\", \"{user.Password}\", \"{user.Email}\"," +
                        $"{(int) user.AuthLevel})";
            bool success;

            MySqlTransaction transaction = null;

            try
            {
                Authentication.Open();
                var command = Authentication.CreateCommand();
                transaction = Authentication.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Authentication.Close();
                success = true;
            }
            catch (MySqlException exception)
            {
                transaction?.Rollback();
                success = false;
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return success;
        }

        public bool Update(User user)
        {
            var query =
                $"Update User SET user_name = \"{user.UserName}\", " +
                $"first_name = \"{user.Firstname}\"," +
                $"last_name = \"{user.Lastname}\"," +
                $"password = \"{user.Password}\"," +
                $"email = \"{user.Email}\"," +
                $"auth_level = \"{user.AuthLevel}\"," +
                $"WHERE user_id = {user.UserId}";

            bool success;

            MySqlTransaction transaction = null;

            try
            {
                Authentication.Open();
                var command = Authentication.CreateCommand();
                transaction = Authentication.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Authentication.Close();
                success = true;
            }
            catch (MySqlException exception)
            {
                transaction?.Rollback();
                success = false;
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return success;
        }

        public bool Delete(int id)
        {
            var query = "UPDATE User SET active_ind =0" +
                        $"WHERE user_id = {id}";
            bool success;

            MySqlTransaction transaction = null;

            try
            {
                Authentication.Open();
                var command = Authentication.CreateCommand();
                transaction = Authentication.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Authentication.Close();
                success = true;
            }
            catch (MySqlException exception)
            {
                transaction?.Rollback();
                success = false;
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return success;
        }

        public IList<User> SelectAll()
        {
            const string query = "SELECT * FROM User WHERE active_ind=1";

            var userList = new List<User>();

            try
            {
                Authentication.Open();
                var command = Authentication.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;

                // We should switch to this using pattern for the connection as well.
                // This implements IDisposable which takes care of closing the connection for us.
                using var inputStream = command.ExecuteReader();
                while (inputStream.Read())
                {
                    var userId = inputStream.GetInt32(0);
                    var userName = inputStream.GetString(1);
                    var firstName = inputStream.GetString(2);
                    var lastname = inputStream.GetString(3);
                    var userPassword = inputStream.GetString(4);
                    var email = inputStream.GetString(5);
                    var activeInd = inputStream.GetBoolean(6);
                    var authLevel = inputStream.GetInt32(7);
                    userList.Add(new User(userId, userName, firstName, lastname, userPassword, email, activeInd,
                        (AuthLevel) authLevel));
                }

                Authentication.Close();
            }
            catch (MySqlException exception)
            {
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return userList;
        }

        public User SelectRow(int id)
        {
            var query = $"SELECT * FROM User WHERE user_id={id} AND active_ind=1";

            User user = null;

            try
            {
                Authentication.Open();
                var command = Authentication.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;

                // We should switch to this using pattern for the connection as well.
                // This implements IDisposable which takes care of closing the connection for us.
                using var inputStream = command.ExecuteReader();
                while (inputStream.Read())
                {
                    var userId = inputStream.GetInt32(0);
                    var userName = inputStream.GetString(1);
                    var firstName = inputStream.GetString(2);
                    var lastname = inputStream.GetString(3);
                    var userPassword = inputStream.GetString(4);
                    var email = inputStream.GetString(5);
                    var activeInd = inputStream.GetBoolean(6);
                    var authLevel = inputStream.GetInt32(7);
                    user = new User(userId, userName, firstName, lastname, userPassword, email, activeInd,
                        (AuthLevel) authLevel);
                }

                Authentication.Close();
            }
            catch (MySqlException exception)
            {
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return user;
        }
    }
}