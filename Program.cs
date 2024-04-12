using BuildsByBrickwellNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddAuthentication();*/

var connectionString = builder.Configuration.GetConnectionString("IntexConnection");
var dbPassword = builder.Configuration["DbPassword"];


connectionString += $"Password={dbPassword};";

// Add services to the container.


var configuration = builder.Configuration;

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var settings = config.Build();
    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(settings["ConnectionStrings:AppConfig"])
               .Select(KeyFilter.Any, LabelFilter.Null)
               .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName);
    });
});

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IntexProjectContext>(options =>
{
    options.UseSqlServer(
        connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, // Maximum number of retries on transient failures.
                maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries.
                errorNumbersToAdd: null // SQL error numbers to be considered as transient. Leave as 'null' for defaults.
            );
        }
    );
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IntexProjectContext>();

builder.Services.AddAuthorization(options =>
{
    // Policy for administrative actions
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
    // Policy for customer actions
    options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
});

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new List<string> { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.Use(async (context, next) => {
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
        "font-src 'self' https://fonts.gstatic.com https://cdnjs.cloudflare.com; " +
        "img-src 'self' data: https://www.lego.com https://images.brickset.com https://m.media-amazon.com https://www.brickeconomy.com; " +
        "frame-src 'self'; " +
        "connect-src 'self' http://localhost:* wss://localhost:* ws://localhost:*");
    await next();
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) => {
    var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
    if (context.User.Identity.IsAuthenticated)
    {
        var user = await userManager.GetUserAsync(context.User);
        var isAdmin = user != null && await userManager.IsInRoleAsync(user, "Admin");
        context.Items["IsAdmin"] = isAdmin; // Store admin status in HttpContext
    }
    await next();
});

app.MapControllerRoute("pagenumandtype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
/*app.MapControllerRoute("pagenumandcolor", "{productColor}/Page{pageNum}", new { Controller = "Home", Action = "Products" });*/
app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "product",
    pattern: "Products/{productType?}/{productColor?}/Page{pageNum}",
    defaults: new { Controller = "Home", Action = "Products", pageNum = 1 }
);
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });


app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();