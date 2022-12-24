using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
#region Settings
// Add Setting from appsettings to model.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{Environments.Development}.json", optional: true);
var logPath = Path.Combine("Logs", "log.txt");
builder.Logging.AddSerilog(new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, shared: true, restrictedToMinimumLevel: LogEventLevel.Information, retainedFileTimeLimit: TimeSpan.FromDays(90))
                .CreateLogger());
#endregion
#region Service and Repositories
#region DB Context
builder.Services.AddDbContext<MuetongContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Muetong")));
#endregion
#region Services
#endregion
#region Repositories
#endregion

#region Essential Services
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
#endregion
#endregion
#region Build App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion