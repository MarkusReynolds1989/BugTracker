namespace BugTracker.Models;

public class User
{
    public string              UserName            { get; set; }
    public string              FirstName           { get; set; }
    public string              LastName            { get; set; }
    public string              Email               { get; set; }
    public string              Password            { get; set; }
    public bool                ActiveIndicator     { get; set; }
    public AuthenticationLevel AuthenticationLevel { get; set; }
    public int                 UserId              { get; set; }

    // For most everything that's not creating a user.
    public User(string              userName,
                string              firstName,
                string              lastName,
                string              email,
                string              password,
                bool                activeIndicator,
                AuthenticationLevel authenticationLevel,
                int                 userId)
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

    // For update.
    public User(string              userName,
                string              firstName,
                string              lastName,
                string              email,
                AuthenticationLevel authenticationLevel,
                int                 userId)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AuthenticationLevel = authenticationLevel;
        UserId = userId;
    }

    // For creating users.
    public User(string              userName,
                string              firstName,
                string              lastName,
                string              email,
                string              password,
                bool                activeIndicator,
                AuthenticationLevel authenticationLevel)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ActiveIndicator = activeIndicator;
        AuthenticationLevel = authenticationLevel;
    }


    public User()
    {
    }
};