namespace BugTracker.Controllers;

public class LoginController
{
    // Dependency injection.
    private readonly IConfiguration _configRoot;

    public LoginController(IConfiguration configRoot)
    {
        _configRoot = configRoot;
    }

    // Set a session to a user from the user that is logged in.
    public async Task<User?> AuthorizeUser(string userName, string password)
    {
        //var hashedPassWord =

        using var hash = SHA256.Create();
        var hashedPassword = BitConverter.ToString(
            hash.ComputeHash(Encoding.Unicode.GetBytes(password))
        );
        try
        {
            await using var authenticationConnection =
                new MySqlConnection(_configRoot.GetConnectionString("default"));

            authenticationConnection.Open();

            var result =
                await authenticationConnection.QueryAsync<User>(
                    "select * from user where UserName = @UserName and Password = @Password",
                    new
                    {
                        UserName = new DbString {Value = userName, IsFixedLength = true, Length = 45, IsAnsi = false},
                        Password = new DbString
                            {Value = hashedPassword, IsFixedLength = true, Length = 100, IsAnsi = false}
                    });
            if (result.Any())
            {
                return result.First();
            }
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
        }

        return null;
    }
}