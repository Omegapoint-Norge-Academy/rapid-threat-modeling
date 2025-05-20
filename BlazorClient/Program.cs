using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Rtm.BlazorClient;
using Rtm.BlazorClient.Components;
using Rtm.BlazorClient.Extensions;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("nb-NO");

var builder = WebApplication.CreateBuilder(args);

// Authentication and authorization middlewares
builder.AddAuthServices();

// Services
builder.Services.AddApplicationServices();

// Cache
builder.Services.AddHybridCache();

// API Controllers
builder.Services.AddControllers();

// Http and Blazor
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database
builder.AddDatabase();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Configure the HTTP request pipeline.
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

// Add login and logout endpoints
var group = app.MapGroup("/authentication").MapGroup("");
group.MapGet("/login", (string? returnUrl) => TypedResults.Challenge(AuthHelper.GetAuthProperties(returnUrl)))
    .AllowAnonymous();
group.MapPost("/logout",
    ([FromForm] string? returnUrl) => TypedResults.SignOut(AuthHelper.GetAuthProperties(returnUrl),
        [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]));

app.Run();