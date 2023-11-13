namespace BugTracker.Pages;

public class Users : PageModel
{
    private readonly IConfiguration _configRoot;
    private readonly ILogger _logger;

    public Users(IConfiguration configRoot)
    {
        _configRoot = configRoot;
    }

    public async Task<IActionResult> OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var authLevel = HttpContext.Session.GetInt32("UserAuthLevel");

        if (userId == null && authLevel == null || authLevel < 2)
        {
            return new RedirectResult("Login?statusCode=401");
        }

        var userController = new UserController(_configRoot);
        var users = await userController.GetAllUsers();
        if (!users.Any()) return new EmptyResult();

        ViewData["Users"] = users;
        return new PageResult();
    }
}