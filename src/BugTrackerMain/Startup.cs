using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace BugTracker;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public Startup(IConfiguration configuration, ILogger logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSession();
        services.AddLogging();
        services.AddMemoryCache();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
            CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Login");
                options.AccessDeniedPath = new PathString("/auth/denied");
            });

        services.AddMvc().AddRazorPagesOptions(options => { options.Conventions.AddPageRoute("/Login", ""); });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Run method on app so we can use sessions for login info.
        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}