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
            using var connection = new DataConnection(_configRoot, 10);
            _ = await connection.ExecuteAsync(
                @"
                insert into User (user_name, first_name, last_name, password, email, auth_level)
                values (@UserName, @FirstName, @LastName, @ThisPassword, @ThisEmail, @AuthenticationLevel)",
                new
                {
                    user.UserName,
                    user.FirstName,
                    user.LastName,
                    ThisPassword = hashedPassword,
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
            using var connection = new DataConnection(_configRoot, 10);
            _ = await connection.ExecuteAsync(
                @"
                update user 
                set first_name = @FirstName,
                    last_name = @LastName,
                    password = @ThisPassword,
                    email = @ThisEmail,
                    active_ind = @ActiveIndicator,
                    auth_level = @AuthenticationLevel
                where user_id = @UserId",
                new
                {
                    user.FirstName,
                    user.LastName,
                    ThisPassword = user.Password,
                    ThisEmail = user.Email,
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
        using var connection = new DataConnection(_configRoot, 10);
        return await connection.QueryAsync<User>("select * from user");
    }

    public async Task<User?> GetUser(int userId)
    {
        using var connection = new DataConnection(_configRoot, 10);
        var result = await connection.QueryAsync<User>(
            "select * from user where user_id = @Id",
            new { Id = userId }
        );

        var user = result as User[] ?? result.ToArray();
        return user.Length == 1 ? user.First() : null;
    }
}
