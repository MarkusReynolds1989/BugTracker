using BugTracker.Controllers;
using BugTracker.Models;
using Xunit;

namespace BugTracker.BugTrackerTest
{
    public class UserControllerTests
    {
        private readonly IDataProcess<User> _userConnection = new UserController();

        [Fact]
        // Test that the connection is correct.
        public void UserConnectionTest()
        {
            Assert.True(_userConnection.Init());
        }

        // Test the insert function of the controller. Need to figure out how to run this without
        // actually hitting the database.
        [Fact]
        public void UserInsertTest()
        {
            Assert.True(_userConnection.Init());
            Assert.True(_userConnection.Insert(new User("Tom", "password123")));
        }

        [Fact]
        public void UserUpdateTest()
        {
            Assert.True(_userConnection.Init());
        }

        // Only run this test when we have to because we will have to query or hardcore that value everytime.
        [Fact]
        public void UserDeleteTest()
        {
            Assert.True(_userConnection.Init());
            Assert.True(_userConnection.Delete(new User(6, null, null, true)));
        }

        [Fact]
        public void UserSelectAllTest()
        {
            Assert.True(_userConnection.Init());
            Assert.True(_userConnection.SelectAll().Count > 0);
        }

        [Fact]
        public void UserSelectRowTest()
        {
            Assert.True(_userConnection.Init());
            Assert.True(_userConnection.SelectRow(1) != null);
        }
    }
}