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
<<<<<<< HEAD
            var login = new LoginController("Tom", "password123");
=======
            var login = new LoginController("markus", "password123");
>>>>>>> 3aa5fcdbbfbb43a88ad4d5bb084fa4d02900e724
            Assert.NotNull(login.AuthorizeUser());
        }
    }
}