using BugTracker.Controllers;
using BugTracker.Models;
using Xunit;

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
            Assert.True(_ticketController.Insert(new Ticket());
        }

        [Fact]
        public void UserUpdateTest()
        {
            Assert.True(_ticketController.Init());
        }

        // Only run this test when we have to because we will have to query or hardcore that value everytime.
        [Fact]
        public void UserDeleteTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.Delete(new User(6, null, null, true)));
        }

        [Fact]
        public void UserSelectAllTest()
        {
            Assert.True(_ticketController.Init());
            Assert.True(_ticketController.SelectAll().Count > 0);
        }
    } 
}