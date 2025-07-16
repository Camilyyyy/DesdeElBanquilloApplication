

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("api", client =>
{
    // Asegúrate de que este puerto (5062) sea el correcto para tu DEAApi.
    // Lo puedes verificar en las propiedades de depuración del proyecto DEAApi.
    client.BaseAddress = new Uri("http://localhost:5062");
});
// ---------------


// Add services to the container.
builder.Services.AddControllersWithViews();

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