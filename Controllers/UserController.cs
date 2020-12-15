using System;
using System.Collections.Generic;
using System.Data;
using BugTracker.Models;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class UserController : IDataProcess<User>
    {
        public MySqlConnectionStringBuilder ConnectionString { get; set; }
        public MySqlConnection Connection { get; set; }

        public bool Init()
        {
            // This is temporary, and when we go to prod we will change this.
            ConnectionString = new MySqlConnectionStringBuilder
            {
                UserID = "markus", Password = "password123", Database = "bug_tracker",
                Server = "***REMOVED***"
            };
            Connection = new MySqlConnection(ConnectionString.ConnectionString);
            // Open the connection.
            Connection.Open();
            // If we are able to connect then we know our connection worked, otherwise we should close. 
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                return true;
            }
            
            Connection.Close();
            return false;
        }

        public bool Insert(User user)
        {
            var query = "INSERT into User (name, password) " +
                        $"VALUES (\"{user.Name}\", \"{user.Password}\")";
            bool success;

            MySqlTransaction transaction = null;
            
            try
            {
                Connection.Open();
                var command = Connection.CreateCommand();
                transaction = Connection.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Connection.Close();
                success = true;
            }
            catch (MySqlException exception)
            {
                transaction?.Rollback();
                success = false;
                Connection.Close();
                Console.WriteLine(exception);
            }

            return success;
        }

        public bool Update(User user)
        {
            //TODO: Satisfy the where clause.
            // Will need to add some sort of functionality for it.
            var query = "UPDATE User (name, password, active_ind) " +
                        $"VALUES (\"{user.Name}\", \"{user.Password}\", \"{user.ActiveInd}\")";
            bool success;

            MySqlTransaction transaction = null;
            
            try
            {
                Connection.Open();
                var command = Connection.CreateCommand();
                transaction = Connection.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Connection.Close();
                success = true;
            }
            catch (MySqlException e)
            {
                transaction?.Rollback();
                success = false;
                Connection.Close();
                Console.WriteLine(e);
            }

            return success;
        }

        public bool Delete(User user)
        {
            
            var query = "Delete from User " +
                        $"where user_id ={user.UserId}";
            bool success;

            MySqlTransaction transaction = null;

            try
            {
                Connection.Open();
                var command = Connection.CreateCommand();
                transaction = Connection.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.ExecuteNonQuery();
                transaction.Commit();
                Connection.Close();
                success = true;
            }
            catch (MySqlException e)
            {
                transaction?.Rollback();
                success = false;
                Connection.Close();
                Console.WriteLine(e);
            }

            return success;
        }



        public IList<User> SelectAll()
        {

            var query = "Select * from User ";

            var userList = new List<User>();        

            MySqlTransaction transaction = null;

            try
            {
                Connection.Open();
                var command = Connection.CreateCommand();
                transaction = Connection.BeginTransaction();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
               var inputStream = command.ExecuteReader();
               while (inputStream.HasRows)
                {
                    
                }
                transaction.Commit();
                Connection.Close();
                
            }
            catch (MySqlException e)
            {
                transaction?.Rollback();
                
                Connection.Close();
                Console.WriteLine(e);
            }

            return null;
        }
    }
}