using System.Runtime.CompilerServices;

namespace BugTracker.Models
{
    public class User
    {
        public int UserId { get; }
        public string UserName { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public string Password { get; }
        public string Email { get; }
        public bool ActiveInd { get; }
        public AuthLevel AuthLevel { get; }

        // This constructor is for a user that already exists.
        public User(int userId, string userName, string firstname, string lastname, string password, string email,
            bool activeInd, AuthLevel authLevel)
        {
            UserId = userId;
            UserName = userName;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
            Email = email;
            ActiveInd = activeInd;
            AuthLevel = authLevel;
        }

        // This constructor is for creating a new user.
        public User(string userName, string firstName, string lastname, string password, string email,
            AuthLevel authLevel)
        {
            UserName = userName;
            Firstname = firstName;
            Lastname = lastname;
            Password = password;
            Email = email;
            ActiveInd = true;
            AuthLevel = authLevel;
        }
    }
}