// Reemplaza todo el contenido de tu Program.cs en el proyecto DEAApi con esto:

using Microsoft.EntityFrameworkCore;
using DEAApi.Data; // Aseg�rate que este sea el namespace correcto de tu DbContext

var builder = WebApplication.CreateBuilder(args);

// --- PASO 1: A�ADIR SERVICIOS AL CONTENEDOR ---

// Registra el DbContext para que la aplicaci�n sepa c�mo conectarse a la base de datos.
// Lee la cadena de conexi�n desde appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); // O .UseSqlite() si usas SQLite en la API

// Registra los servicios para que los controladores de la API funcionen.
builder.Services.AddControllers();

// Opcional pero recomendado: A�ade Swagger/OpenAPI para poder probar tu API f�cilmente desde el navegador.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- PASO 2: CONSTRUIR LA APLICACI�N ---

var app = builder.Build();


// --- PASO 3: CONFIGURAR EL PIPELINE DE SOLICITUDES HTTP ---

// Configura Swagger solo en el entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura el enrutamiento para que las solicitudes lleguen a tus controladores.
app.UseAuthorization();
app.MapControllers();


// --- PASO 4: EJECUTAR LA APLICACI�N ---

app.Run();