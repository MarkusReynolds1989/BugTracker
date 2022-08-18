using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers {
    public class TicketController {
        private readonly IConfiguration _configRoot;

        public TicketController(IConfiguration configRoot) {
            _configRoot = configRoot;
        }

        public async Task<bool> Insert(Ticket ticket) {
            bool success;

            try {
                using var connection = new DataConnection(_configRoot);
                await using var command = new MySqlCommand("AddTicket", await connection.Connect());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WorkerId", ticket.WorkerId);
                command.Parameters.AddWithValue("@ThisTitle", ticket.Title);
                command.Parameters.AddWithValue("@ThisDescription", ticket.Description);
                command.Parameters.AddWithValue("@LoggerId", ticket.LoggerId);
                await command.ExecuteNonQueryAsync();
                success = true;
            }
            catch (MySqlException exception) {
                success = false;
                Debug.WriteLine(exception);
            }

            return success;
        }

        public async Task<bool> Update(Ticket ticket) {
            bool success;

            try {
                using var connection = new DataConnection(_configRoot);
                await using var command = new MySqlCommand("UpdateTicket", await connection.Connect());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TicketId", ticket.TicketId);
                command.Parameters.AddWithValue("@WorkerId", ticket.WorkerId);
                command.Parameters.AddWithValue("@ThisTitle", ticket.Title);
                command.Parameters.AddWithValue("@ThisDescription", ticket.Description);
                command.Parameters.AddWithValue("@ThisResolution", ticket.Resolution);
                command.Parameters.AddWithValue("@StatusInd", ticket.StatusIndCd);
                await command.ExecuteNonQueryAsync();
                success = true;
            }
            catch (MySqlException ex) {
                Debug.WriteLine(ex);
                success = false;
            }

            return success;
        }

        public async Task<Ticket?> GetTicket(int ticketId) {
            Ticket? ticket = null;
            try {
                var connection = new DataConnection(_configRoot, null, null, 10, ConnectionState.Open);
                var result =
                    await connection.QueryAsync<Ticket>("select * from Ticket where ticket_id = @Id"
                                                      , new {Id = ticketId});
                return result.First();
            }
            catch (MySqlException ex) { Debug.WriteLine(ex); }

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByWorkerId(int workerId) {
            IList<Ticket> output = new List<Ticket>();
            try {
                using var connection = new DataConnection(_configRoot);
                await using var command = new MySqlCommand("GetAllTicketsForWorker", await connection.Connect());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", workerId);
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows) {
                    while (await reader.ReadAsync()) {
                        output.Add(new Ticket(
                                       reader.GetInt32(1),
                                       reader.GetString(2),
                                       reader.GetString(3),
                                       reader.GetString(4),
                                       reader.GetInt32(7),
                                       (StatusIndCd) reader.GetInt32(5),
                                       reader.GetInt32(0)));
                    }
                }
            }
            catch (MySqlException ex) { Debug.WriteLine(ex); }

            return output;
        }
    }
}