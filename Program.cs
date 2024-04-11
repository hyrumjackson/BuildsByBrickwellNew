using BuildsByBrickwellNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();

var connectionString = builder.Configuration.GetConnectionString("IntexConnection");
var dbPassword = builder.Configuration["DbPassword"];

connectionString += $"Password={dbPassword};";

// Add services to the container.
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute("pagenumandtype", "{productType}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
/*app.MapControllerRoute("pagenumandcolor", "{productColor}/Page{pageNum}", new { Controller = "Home", Action = "Products" });*/
app.MapControllerRoute(
    name: "product",
    pattern: "Products/{productType?}/{productColor?}/Page{pageNum}",
    defaults: new { Controller = "Home", Action = "Products", pageNum = 1 }
);
app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();