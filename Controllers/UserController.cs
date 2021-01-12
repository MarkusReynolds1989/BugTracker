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
            var query = "INSERT INTO User (name, password) " +
                        $"VALUES (\"{user.Name}\", \"{user.Password}\")";
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
                $"Update User SET name = \"{user.Name}\", " +
                $"password = \"{user.Password}\" " +
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

        public bool Delete(User user)
        {
            var query = $"DELETE FROM User WHERE user_id ={user.UserId}";
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
            const string query = "SELECT * FROM User ";

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
                    var userPassword = inputStream.GetString(2);
                    var activeInd = inputStream.GetBoolean(3);
                    userList.Add(new User(userId, userName, userPassword, activeInd));
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
            var query = $"SELECT * FROM User WHERE user_id={id}";

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
                    var userPassword = inputStream.GetString(2);
                    var activeInd = inputStream.GetBoolean(3);
                    user = new User(userId, userName, userPassword, activeInd);
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