namespace BugTracker.Pages;

public class Login : PageModel
{
    private readonly IConfiguration _config;
    private readonly ILogger _logger;

    public Login(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
    }

    [BindProperty, Required, MaxLength(45)]
    public string UserName { get; }

    [BindProperty, Required, MaxLength(45)]
    public string Password { get; }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return new RedirectToPageResult("");
        }

        var login = new LoginController(_config, _logger); 

        var userName = Request.Form["UserName"].ToString();
        var password = Request.Form["Password"].ToString();
        var user = await login.AuthorizeUser(userName, password);
        // Set the users session user id here.
        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.UserId);
        }
        else
        {
            return new ForbidResult();
        }

        HttpContext.Session.SetInt32("UserAuthLevel", (int) user.AuthenticationLevel);
        return new RedirectToPageResult("Tickets");
    }
}