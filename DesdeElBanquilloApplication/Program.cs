using DesdeElBanquilloApplication.Data;
using DesdeElBanquilloApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SQL Server (producción / staging)
builder.Services.AddDbContext<DesdeElBanquilloAppDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DesdeElBanquilloAppDBContext")
    )
);

// SQLite (desarrollo local)
builder.Services.AddDbContext<DesdeElBanquilloAppSQLiteContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DesdeElBanquilloAppSQLiteContext")
    )
);

// Añade MVC con vistas
builder.Services.AddControllersWithViews();

// Registrar servicio de Ligas
builder.Services.AddScoped<ILigaService, LigaService>();

var app = builder.Build();

// Configura el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rutas convencionales MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();