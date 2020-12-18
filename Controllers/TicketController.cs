using System;
using System.Collections.Generic;
using System.Data;
using BugTracker.Models;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class TicketController : IDataProcess<Ticket>
    {
        public MySqlConnectionStringBuilder AuthenticationString { get; set; }
        public MySqlConnection Authentication { get; set; }

        public bool Init()
        {
            // This is temporary, and when we go to prod we will change this.
            AuthenticationString = new MySqlConnectionStringBuilder
            {
                UserID = "markus", Password = "password123", Database = "bug_tracker",
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

        public bool Insert(Ticket ticket)
        {
            var query = "INSERT INTO Ticket (title, description, resolution) " +
                        $"VALUES (\"{ticket.Title}\", \"{ticket.Description}\", \"{ticket.Resolution}\")";
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

        public bool Update(Ticket ticket)
        {
            // TODO: Consider configuring this query to where the code can change a ticket_id.
            var query = "UPDATE Ticket (title, description, resolution, statusindcd) " +
                        $"VALUES (\"{ticket.Title}\", \"{ticket.Description}\", \"{ticket.Resolution}\", \"{ticket.StatusIndCd}\")" +
                        $"WHERE ticket_id = {ticket.TicketId}";

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

        public bool Delete(Ticket ticket)
        {
            var query = $"DELETE FROM Ticket WHERE ticket_id ={ticket.TicketId}";
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


        public IList<Ticket> SelectAll()
        {
            const string query = "SELECT * FROM Ticket ";

            var ticketList = new List<Ticket>();

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
                    var ticketId = inputStream.GetInt32(0);
                    var workerId = inputStream.getInt32(1);
                    var loggerId = inputStream.GetInt32(2);
                    var ticketTitle = inputStream.GetString(3);
                    var ticketDescription = inputStream.GetString(4);
                    var ticketResolution = inputStream.GetString(5);
                    var ticketStatusIndCd = (StatusIndCd)inputStream.GetInt32(6); // I know GetEnum() is wrong but I'm not sure what it should be
                    ticketList.Add(new Ticket(ticketId, workerId, loggerId,ticketTitle,ticketDescription,ticketResolution,ticketStatusIndCd));
                }

                Authentication.Close();
            }
            catch (MySqlException exception)
            {
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return ticketList;
        }
    }
}