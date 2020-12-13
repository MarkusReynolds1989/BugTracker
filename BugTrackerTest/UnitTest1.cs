using BugTracker.Controllers;
using BugTracker.Models;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BugTracker.BugTrackerTest
{
    public class Tests
    {
        private readonly UserController _userConnection = new UserController();
        
        [Fact]
        // Test that the connection is correct.
        public void UserConnectionTest()
        {
            Assert.IsTrue(_userConnection.Init());
        }
        
        // Test the insert function of the controller. Need to figure out how to run this without
        // actually hitting the database.
        [Fact]
        public void UserInsertTest()
        {
            Assert.IsTrue(_userConnection.Init());
            Assert.IsTrue(_userConnection.Insert(new User("Tom", "password123")));
        }

        [Fact]
        public void UserUpdateTest()
        {
            Assert.IsTrue(_userConnection.Init());
        }
    }
}
