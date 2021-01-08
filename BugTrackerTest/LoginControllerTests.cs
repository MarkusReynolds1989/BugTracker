using BugTracker.Controllers;
using BugTracker.Models;
using Xunit;

namespace BugTracker.BugTrackerTest
{
    public class LoginControllerTests
    {
        [Fact]
        public void LoginTest()
        {
            var login = new LoginController("markus", "password123");
            Assert.NotNull(login.AuthorizeUser());
        }
    }
}