using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Penalty_Calculation1.Models;

 

var builder = WebApplication.CreateBuilder(args);

 

// Add services to the container.
builder.Services.AddControllersWithViews();

 

// Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

 

// Enable legacy timestamp behavior for Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

 

builder.Services.AddDbContext<PenaltyCalculationContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("Server=localhost;Port=5432;Database=Penalty_Calculation;User Id=postgres;Password=Priti@12345")));

 

var app = builder.Build();

 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

 

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

 

app.Run();