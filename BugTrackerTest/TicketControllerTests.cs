using BugTracker.Controllers;
using BugTracker.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BugTracker.BugTrackerTest
{
    public class TicketControllerTests
    {
        private readonly IDataProcess<Ticket> _ticketController = new TicketController();

        [Fact]
        // Test that the connection is correct.
        public void TicketConnectionTest()
        {
            Assert.True(_ticketController.Init());
        }

        // Test the insert function of the controller. Need to figure out how to run this without
        // actually hitting the database.
        [Fact]
        public void TicketInsertTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.Insert(new Ticket(0, "NewTicket", "Test Ticket", "", StatusIndCd.Open, 0)));
        }

        [Fact]
        public void TicketUpdateTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.Update(new Ticket(0,0,"title","Test ticket","resolve",StatusIndCd.Closed,0)));
        }

        // Only run this test when we have to because we will have to query or hardcore that value everytime.
        [Fact]
        public void TicketDeleteTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.Delete(new Ticket(0,0,null,null,null,0,0)));
        }

        [Fact]
        public void TicketSelectAllTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.SelectAll().Count > 0);
        }

        [Fact]
        public void TicketSelectRowTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.SelectRow(1) != null);
        }
    } 
}