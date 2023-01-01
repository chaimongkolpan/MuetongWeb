using Microsoft.EntityFrameworkCore;
using MuetongWeb.Models.Entities;
using MuetongWeb.Services;
using MuetongWeb.Services.Interfaces;
using MuetongWeb.Repositories;
using MuetongWeb.Repositories.Interfaces;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
#region Settings
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
#region Services and Repositories
#region DB Context
builder.Services.AddDbContext<MuetongContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Muetong")));
#endregion
#region Services
builder.Services.AddScoped<IBillingServices, BillingServices>();
builder.Services.AddScoped<IContractorServices, ContractorServices>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IPoServices, PoServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IProjectServices, ProjectServices>();
builder.Services.AddScoped<IPrServices, PrServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<ISettingServices, SettingServices>();
builder.Services.AddScoped<IStoreServices, StoreServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IWorkLineServices, WorkLineServices>();
#endregion
#region Repositories
builder.Services.AddScoped<IBillingRepositories, BillingRepositories>();
builder.Services.AddScoped<IContractorRepositories, ContractorRepositories>();
builder.Services.AddScoped<ICustomerRepositories, CustomerRepositories>();
builder.Services.AddScoped<IDepartmentRepositories, DepartmentRepositories>();
builder.Services.AddScoped<IFileRepositories, FileRepositories>();
builder.Services.AddScoped<ILineRepositories, LineRepositories>();
builder.Services.AddScoped<IPaymentAccountRepositories, PaymentAccountRepositories>();
builder.Services.AddScoped<IPoRepositories, PoRepositories>();
builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
builder.Services.AddScoped<IProjectRepositories, ProjectRepositories>();
builder.Services.AddScoped<IProvinceRepositories, ProvinceRepositories>();
builder.Services.AddScoped<IPrRepositories, PrRepositories>();
builder.Services.AddScoped<IRolePermissionRepositories, RolePermissionRepositories>();
builder.Services.AddScoped<IRoleRepositories, RoleRepositories>();
builder.Services.AddScoped<ISettingConstantRepositories, SettingConstantRepositories>();
builder.Services.AddScoped<IStoreRepositories, StoreRepositories>();
builder.Services.AddScoped<ISubDepartmentRepositories, SubDepartmentRepositories>();
builder.Services.AddScoped<IUserRepositories, UserRepositories>();
#endregion
#region Essential Services
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("SessionTimeout"));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion
#endregion
#region Build App
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion