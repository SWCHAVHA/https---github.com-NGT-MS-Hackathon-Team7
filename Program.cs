using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Penalty_Calculation1.Models;
using Serilog;

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
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"C:\Users\SWCHAVHA\OneDrive - Capgemini\Documents\LoggError\logs.txt", rollingInterval: RollingInterval.Day)

    .CreateLogger();

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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "text/plain";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;

        // Log the exception using Serilog
        Log.Error(exception, "An unhandled exception occurred");

        // Write the exception message to the response
        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=HomeView}/{id?}");

app.Run();
