using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Rtm.BlazorClient.Extensions;

public static class AuthServiceExtensions
{
    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
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

        builder.Services.AddAuthorization();
    }
}