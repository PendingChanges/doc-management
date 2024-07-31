using System;

namespace Doc.Management.Api;

public class KeycloakAuthenticationOptions
{
    public const string Section = "Keycloak";

    public TimeSpan TokenClockSkew { get; set; }
    public bool? VerifyTokenAudience { get; set; }
    public string? SslRequired { get; set; }
    public string? KeycloakUrlRealm { get; set; }
    public string? Resource { get; set; }
}
