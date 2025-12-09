using Microsoft.EntityFrameworkCore;
using ZambeelApp.Components;
using ZambeelApp.Models;   // Needed for ZambeelPlusContext
using ZambeelApp.Services; // Needed for IZambeelService

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// 1. REGISTER DATABASE CONTEXT
// This reads the "DefaultConnection" string from appsettings.json
// =========================================================
builder.Services.AddDbContext<ZambeelPlusContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =========================================================
// 2. REGISTER YOUR SERVICES (Dependency Injection)
// =========================================================
// Default: Use Stored Procedure implementation
builder.Services.AddScoped<IZambeelService, Service_StoredProcs>();

// Register LINQ implementation separately (so we can swap manually if needed)
builder.Services.AddScoped<Service_LINQ>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();