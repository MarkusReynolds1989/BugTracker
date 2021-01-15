using System.Runtime.CompilerServices;

namespace BugTracker.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public bool ActiveInd { get; private set; }
        public AuthCd AuthCd { get; private set; }

        // This constructor is for a user that already exists.
        public User(int userId, string name, string password, bool activeInd)
        {
            UserId = userId;
            Name = name;
            Password = password;
            ActiveInd = activeInd;
        }
        
        // This constructor is for creating a new user.
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}