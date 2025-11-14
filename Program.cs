using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using EventCardCopilotApp.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Registrazione dei servizi
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ✅ Accesso a HttpContext nei servizi
builder.Services.AddHttpContextAccessor();

// ✅ Autenticazione con cookie
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyAuthCookie";
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2); // ⏱️ cookie valido 2 minuti
    });

// ✅ Autorizzazione
builder.Services.AddAuthorization();

// ✅ TempData per messaggi tra redirect
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddHttpClient();
// ✅ Servizi personalizzati
builder.Services.AddScoped<PhotoService>(); 
builder.Services.AddScoped<UserService>(); 
builder.Services.AddScoped<EventNewService>(); 
builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.LoginPath = "/login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// 🔧 Middleware
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // ✅ TempData support
app.UseAuthentication(); // 🔐 cookie login
app.UseAuthorization();  // 🔐 [Authorize]

app.MapControllers();

// 🔁 Endpoints
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

