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
            var login = new LoginController();
            Assert.NotNull(login.AuthorizeUser(new User(0, "test", "test", "McTester", "test", "test@test.com",
                true, AuthLevel.Admin)));
        }
    }
}