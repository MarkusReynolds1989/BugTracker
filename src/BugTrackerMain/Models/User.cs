namespace BugTracker.Models;

// UserId is nullable in the situation where we want to create a new user.
// Also default auth level to guest, it is defaulted on the sql side as well.
// Also default active ind, it is defaulted on the sql side as well.
public record User(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string? Password,
    bool? ActiveIndicator,
    AuthLevel? AuthenticationLevel,
    int? UserId
);
