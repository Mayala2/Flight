// File: Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieListApp.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Enforce lower-case URLs (rubric)
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// EF Core + SQLite
builder.Services.AddDbContext<FlightContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieContext")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Bootstrap/css/js from template

app.UseRouting();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Flights}/{action=Index}/{id?}");

app.Run();