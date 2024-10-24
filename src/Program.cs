using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using KontrastDB.Models;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllersWithViews();

var conStrBuilder = new SqlConnectionStringBuilder(
        builder.Configuration.GetConnectionString("KontrastDB"));
conStrBuilder.UserID = builder.Configuration["db_user"];
conStrBuilder.Password = builder.Configuration["db_pass"];
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<KontrastContext>(opts =>
{
    opts.UseMySQL(connection);
});

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();

/* ---- Use this for online
 using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using KontrastDB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Hardcoded username and password
string userId = "USER";
string password = "PASS";

var conStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("KontrastDB"));
conStrBuilder.UserID = userId;
conStrBuilder.Password = password;

var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<KontrastContext>(opts =>
{
    opts.UseMySQL(connection);
});

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
*/