using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models;

// UserId is nullable in the situation where we want to create a new user.
// Also default auth level to guest, it is defaulted on the sql side as well.
// Also default active ind, it is defaulted on the sql side as well.
public class User
{
    public string?    UserName            { get; set; }
    public string?    FirstName           { get; set; }
    public string?    LastName            { get; set; }
    public string?    Email               { get; set; }
    public string?    Password            { get; set; }
    public bool?      ActiveIndicator     { get; set; }
    public AuthenticationLevel? AuthenticationLevel { get; set; }
    public int?       UserId              { get; set; }

    public User(string?    userName,
                string?    firstName,
                string?    lastName,
                string?    email,
                string?    password,
                bool?      activeIndicator,
                AuthenticationLevel? authenticationLevel,
                int?       userId)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ActiveIndicator = activeIndicator;
        AuthenticationLevel = authenticationLevel;
        UserId = userId;
    }

    public User()
    {
    }
};