namespace BugTracker.Pages;

public class CreateTicketModel : PageModel
{
    private IConfiguration _configRoot;

    public CreateTicketModel(IConfiguration configRoot)
    {
        _configRoot = configRoot;
    }

    [BindProperty, Required, MaxLength(45)]
    public string _Title { get; set; }

    [BindProperty, Required, MaxLength(300)]
    public string Description { get; set; }

    public IEnumerable<SelectListItem> Users { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            Response.Redirect("Login");
        }

        if (ModelState.IsValid)
        {
            var ticketController = new TicketController(_configRoot);
            var workerId = int.Parse(Request.Form["WorkerId"]);
            _Title = Request.Form["_Title"];
            Description = Request.Form["Description"];
            var loggerId = int.Parse(Request.Form["LoggerId"]);

            var ticket = new Models.Ticket(
                workerId,
                _Title,
                Description,
                loggerId
            );

            if (await ticketController.Insert(ticket))
            {
                Response.Redirect("/Tickets");
            }
        }

        return Page();
    }

    public void OnGet()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            Response.Redirect("Login");
        }

        // Generate users that can accept tickets.
        var userController = new UserController(_configRoot);
        Users = userController
            .GetAllUsers()
            .Result.Select(
                user =>
                    new SelectListItem
                    {
                        Value = user.UserId.ToString(),
                        Text = user.UserId.ToString()
                    }
            );
        ViewData["Users"] = Users;
    }
}