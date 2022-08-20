using BugTracker.Pages;
using User = BugTracker.Models.User;

namespace BugTracker.Controllers;

public class UserController
{
    private readonly IConfiguration _configRoot;

    public UserController(IConfiguration configRoot)
    {
        _configRoot = configRoot;
    }

    public async Task<bool> Insert(User user)
    {
        bool success;
        using var hash = SHA256.Create();
        var hashedPassword = BitConverter.ToString(
            hash.ComputeHash(Encoding.Unicode.GetBytes(user.Password ?? string.Empty))
        );

        try
        {
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            _ = await connection.ExecuteAsync(
                @"
                insert into user (UserName, FirstName, LastName, Password, Email, AuthenticationLevel)
                values (@UserName, @FirstName, @LastName, @Password, @Email, @AuthenticationLevel)",
                new
                {
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    Password = hashedPassword,
                    user.AuthenticationLevel
                }
            );
            success = true;
        }
        catch (MySqlException exception)
        {
            Debug.WriteLine(exception);
            success = false;
        }

        return success;
    }

    public async Task<bool> Update(User user)
    {
        bool success;

        try
        {
            await using var connection = new MySqlConnection(
                _configRoot.GetConnectionString("default")
            );

            await connection.OpenAsync();

            _ = await connection.ExecuteAsync(
                @"
                update user 
                set FirstName = @FirstName,
                    LastName = @LastName,
                    Password = @Password,
                    Email = @Email,
                    ActiveIndicator = @ActiveIndicator,
                    AuthenticationLevel = @AuthenticationLevel
                where UserId = @UserId",
                new
                {
                    user.FirstName,
                    user.LastName,
                    user.Password,
                    user.Email,
                    user.ActiveIndicator,
                    user.AuthenticationLevel,
                    user.UserId
                }
            );
            success = true;
        }
        catch (MySqlException ex)
        {
            Debug.WriteLine(ex);
            success = false;
        }

        return success;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        await using var connection = new MySqlConnection(
            _configRoot.GetConnectionString("default")
        );

        await connection.OpenAsync();

        return await connection.QueryAsync<User>("select * from user");
    }

    public async Task<User?> GetUser(int userId)
    {
        await using var connection = new MySqlConnection(
            _configRoot.GetConnectionString("default")
        );

        await connection.OpenAsync();

        var result = await connection.QueryAsync<User>(
            "select * from user where UserId = @Id",
            new { Id = userId }
        );

        var user = result as User[] ?? result.ToArray();
        return user.Length == 1 ? user.First() : null;
    }
}
