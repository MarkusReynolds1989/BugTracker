using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class TicketController
    {
        private readonly IConfiguration _configRoot;

        public TicketController(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public bool Insert(Ticket ticket)
        {
            bool success;

            try
            {
                using var connection = new DataConnection(_configRoot);
                connection.Connect();
                using var command = new MySqlCommand("AddTicket", connection.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WorkerId", ticket.WorkerId);
                command.Parameters.AddWithValue("@ThisTitle", ticket.Title);
                command.Parameters.AddWithValue("@ThisDescription", ticket.Description);
                command.Parameters.AddWithValue("@LoggerId", ticket.LoggerId);
                command.ExecuteNonQuery();
                success = true;
            }
            catch (MySqlException exception)
            {
                success = false;
                Debug.WriteLine(exception);
            }

            return success;
        }

        public bool Update(Ticket ticket)
        {
            bool success;

            try
            {
                using var connection = new DataConnection(_configRoot);
                connection.Connect();
                var command = new MySqlCommand("UpdateTicket", connection.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TicketId", ticket.TicketId);
                command.Parameters.AddWithValue("@WorkerId", ticket.WorkerId);
                command.Parameters.AddWithValue("@ThisTitle", ticket.Title);
                command.Parameters.AddWithValue("@ThisDescription", ticket.Description);
                command.Parameters.AddWithValue("@ThisResolution", ticket.Resolution);
                command.Parameters.AddWithValue("@StatusInd", ticket.StatusIndCd);
                command.ExecuteNonQuery();
                success = true;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex);
                success = false;
            }

            return success;
        }
    }
}