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
            await using var command = new MySqlCommand("AddUser", await connection.Connect());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Firstname", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@ThisPassword", hashedPassword);
            command.Parameters.AddWithValue("@ThisEmail", user.Email);
            command.Parameters.AddWithValue("AuthLevel", user.AuthLevel);
            await command.ExecuteNonQueryAsync();
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
            await using var command = new MySqlCommand("UpdateUser", await connection.Connect());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@Firstname", user.FirstName);
            command.Parameters.AddWithValue("@LastName", user.LastName);
            command.Parameters.AddWithValue("@ThisEmail", user.Email);
            command.Parameters.AddWithValue("@ActiveInd", true);
            command.Parameters.AddWithValue("@AuthLevel", user.AuthLevel);
            await command.ExecuteNonQueryAsync();
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
        return await connection.QueryAsync<User>("select * from User");
    }

    public async Task<User?> GetUser(int userId)
    {
        using var connection = new DataConnection(_configRoot, 10);
        var result = await connection.QueryAsync<User>(
            "select * from User where user_id = @Id",
            new { Id = userId }
        );

        var user = result as User[] ?? result.ToArray();
        return user.Length == 1 ? user.First() : null;
    }
}
