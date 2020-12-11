using System.Runtime.CompilerServices;

namespace BugTracker.Models
{
    public class User
    {
        private int UserId { get; set; }
        private string Name { get; set; }
        private string Password { get; set; }
        private bool ActiveInd { get; set; }

        public User(int userId, string name, string password, bool activeInd)
        {
            UserId = userId;
            Name = name;
            Password = password;
            ActiveInd = activeInd;
        }
    }
}