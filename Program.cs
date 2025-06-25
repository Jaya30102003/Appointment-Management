using System.Text.Json.Serialization;
using Appointments.Controllers;
using Appointments.Service;
using Appointments.AutoMapperProfile;
using Appointments.Repository;
using Applications.DbContexts;
using Microsoft.EntityFrameworkCore;
using Notifications.Repository;
using Notifications.Service;
using Notifications.Helper;

var builder = WebApplication.CreateBuilder(args);

// 🛠 Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// 🔁 AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// ⚙️ Services & Repositories
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<NotificationHelper>();

// 🌐 Razor Views + API Controllers
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// 🚦 Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🧭 Routing
app.UseEndpoints(endpoints =>
{
    // Default route — for Razor pages
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=NotificationView}/{action=Selector}/{id?}");

    // Optional: You can map fallback or other custom routes here if needed
});

app.Run();
