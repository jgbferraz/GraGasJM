using Microsoft.EntityFrameworkCore;
using GraGasJM.Application;
using GraGasJM.Data;
using GraGasJM.Infrastructure;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Use um caminho absoluto para evitar criar bancos diferentes por diretório de execução.
var dbPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "gragas.db"));
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Data Source={dbPath}";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Inicializar banco de dados
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
