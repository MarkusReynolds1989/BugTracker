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

        public bool Insert(Ticket ticket)
        {
            var query = "INSERT INTO Ticket (worker_id,title,description,resolution,status_ind,logger_id)" +
                        $"VALUES (\"{ticket.WorkerId}\",\"{ticket.Title}\", \"{ticket.Description}\", \"{ticket.Resolution}\", \"{(int) ticket.StatusIndCd}\",\"{ticket.LoggerId}\")";
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
            var query =
                $"UPDATE Ticket SET worker_id = {ticket.WorkerId}, " +
                $"title = \"{ticket.Title}\", " +
                $"description = \"{ticket.Description}\", " +
                $"resolution = \"{ticket.Resolution}\", " +
                $"status_ind = {(int) ticket.StatusIndCd} " +
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
                    var workerId = inputStream.GetInt32(1);
                    var ticketTitle = inputStream.GetString(2);
                    var ticketDescription = inputStream.GetString(3);
                    var ticketResolution = inputStream.GetString(4);
                    var ticketStatusIndCd = (StatusIndCd) inputStream.GetInt32(5);
                    var loggerId = inputStream.GetInt32(6);
                    ticketList.Add(new Ticket(ticketId, workerId, ticketTitle, ticketDescription, ticketResolution,
                        ticketStatusIndCd, loggerId));
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

        // Implement overload for getting tickets by worker_id.
        public IList<Ticket> SelectAll(int id)
        {
            // Removed const.
            var query = $"SELECT * FROM Ticket WHERE worker_id ={id}";

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
                    var workerId = inputStream.GetInt32(1);
                    var ticketTitle = inputStream.GetString(2);
                    var ticketDescription = inputStream.GetString(3);
                    var ticketResolution = inputStream.GetString(4);
                    var ticketStatusIndCd = (StatusIndCd) inputStream.GetInt32(5);
                    var loggerId = inputStream.GetInt32(6);
                    ticketList.Add(new Ticket(ticketId, workerId, ticketTitle, ticketDescription, ticketResolution,
                        ticketStatusIndCd, loggerId));
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

        public Ticket SelectRow(int id)
        {
            // Removed const.
            var query = $"SELECT * FROM Ticket WHERE ticket_id={id}";

            Ticket ticket = null;

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
                    var workerId = inputStream.GetInt32(1);
                    var ticketTitle = inputStream.GetString(2);
                    var ticketDescription = inputStream.GetString(3);
                    var ticketResolution = inputStream.GetString(4);
                    var ticketStatusIndCd = (StatusIndCd) inputStream.GetInt32(5);
                    var loggerId = inputStream.GetInt32(6);
                    ticket = new Ticket(ticketId, workerId, ticketTitle, ticketDescription, ticketResolution,
                        ticketStatusIndCd, loggerId);
                }

                Authentication.Close();
            }
            catch (MySqlException exception)
            {
                Authentication.Close();
                Console.WriteLine(exception);
            }

            return ticket;
        }
    }
}