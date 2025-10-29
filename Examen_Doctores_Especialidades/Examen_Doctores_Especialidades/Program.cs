var builder = WebApplication.CreateBuilder(args);

// ✅ REGISTRA LOS SERVICIOS MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Doctor}/{action=Index}/{id?}");

app.Run();
