using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Doc.Management.Api;

public static class ServiceCollectionExtensions
{
    public static AuthenticationBuilder AddKeycloackAuthentication(
        this IServiceCollection services,
        KeycloakAuthenticationOptions options,
        Action<JwtBearerOptions>? configureOptions = default
    )
    {
        const string roleClaimType = ClaimTypes.Role;
        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = options.TokenClockSkew,
            ValidateAudience = options.VerifyTokenAudience ?? true,
            ValidateIssuer = true,
            NameClaimType = "preferred_username",
            RoleClaimType = roleClaimType,
        };

        return services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                var sslRequired =
                    string.IsNullOrWhiteSpace(options.SslRequired)
                    || options.SslRequired.Equals("external", StringComparison.OrdinalIgnoreCase);

                opts.Authority = options.KeycloakUrlRealm;
                opts.Audience = options.Resource;
                opts.TokenValidationParameters = validationParameters;
                opts.RequireHttpsMetadata = sslRequired;
                opts.SaveToken = true;
                configureOptions?.Invoke(opts);
            });
    }
}
