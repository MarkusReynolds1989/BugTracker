namespace BugTracker.Controllers;

public class LoginController
{
    private readonly IConfiguration _configRoot;
    private readonly ILogger _logger;

    public LoginController(IConfiguration configRoot, ILogger loggerRoot)
    {
        _configRoot = configRoot;
        _logger = loggerRoot;
    }

    // Set a session to a user from the user that is logged in.
    public async Task<User> AuthorizeUser(string userName, string password)
    {
        using var hash = SHA256.Create();
        var hashedPassword = BitConverter.ToString(
            hash.ComputeHash(Encoding.Unicode.GetBytes(password))
        );
        try
        {
            await using var authenticationConnection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await authenticationConnection.OpenAsync();

            var result = await authenticationConnection.QueryAsync<User>(
                "select * from user where UserName = @UserName and Password = @Password",
                new
                {
                    UserName = new DbString
                    {
                        Value = userName,
                        IsFixedLength = true,
                        Length = 45,
                        IsAnsi = false
                    },
                    Password = new DbString
                    {
                        Value = hashedPassword,
                        IsFixedLength = true,
                        Length = 100,
                        IsAnsi = false
                    }
                }
            );

            return result.First();
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex.Message);
        }

        return null;
    }
}
