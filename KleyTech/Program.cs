using KleyTech.Data;
using KleyTech.DataAccess;
using KleyTech.DataAccess.Data.Repository;
using KleyTech.DataAccess.Data.Repository.IRepository;
using KleyTech.DataAccess.Initializer;
using KleyTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWorkContainer, WorkContainer>();

//Seed data
builder.Services.AddScoped<IInitializerDB, InitializerDB> ();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

SeedData();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


void SeedData() {
    using (var scope = app.Services.CreateScope())
    {
        var initializerDB = scope.ServiceProvider .GetRequiredService<IInitializerDB>();
        initializerDB.Initialize();
    }
}