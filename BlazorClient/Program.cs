using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using BlazorClient;
using BlazorClient.Components;
using Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add authentication middleware
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddMicrosoftIdentityWebApp(options =>
        {
            builder.Configuration.Bind("AzureAd", options);
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.Events.OnRedirectToIdentityProvider = context =>
            {
                // Redirect user to IdP if user tries to access page requiring authentication
                if (context.HttpContext.User.Identity is null ||
                    !context.HttpContext.User.Identity.IsAuthenticated &&
                    !context.Request.Path.Equals("/authentication/login"))
                {
                    context.HandleResponse();
                    context.Response.Redirect("/authentication/login");
                }

                return Task.CompletedTask;
            };
        },
        cookieOptions =>
        {
            cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            cookieOptions.SlidingExpiration = true;
            cookieOptions.Cookie.HttpOnly = true;
            cookieOptions.Cookie.IsEssential = true;
            cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            cookieOptions.Cookie.SameSite = SameSiteMode.Lax;
        });

// Add authorization and set default policy
builder.Services.AddAuthorization(options =>
{
    var defaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy("AuthenticatedUser", defaultPolicy);
    options.DefaultPolicy = defaultPolicy;
    options.FallbackPolicy = defaultPolicy;
});

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<CommercialContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString,
            s => s.MigrationsAssembly(typeof(CommercialContext).Assembly));
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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

// Add login and logout endpoints
var group = app.MapGroup("/authentication").MapGroup("");
group.MapGet("/login", (string? returnUrl) => TypedResults.Challenge(AuthHelper.GetAuthProperties(returnUrl)))
    .AllowAnonymous();
group.MapPost("/logout",
    ([FromForm] string? returnUrl) =>
    {
        return TypedResults.SignOut(AuthHelper.GetAuthProperties(returnUrl),
            [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]);
    });


app.Run();