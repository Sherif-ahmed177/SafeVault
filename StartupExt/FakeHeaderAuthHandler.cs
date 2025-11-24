using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SafeVault.StartupExt;

public class FakeHeaderAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeHeaderAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Look for X-User-Name and X-User-Role headers (demo only - do not use in production)
        if (!Request.Headers.ContainsKey("X-User-Name"))
            return Task.FromResult(AuthenticateResult.NoResult());

        var name = Request.Headers["X-User-Name"].FirstOrDefault() ?? "demo";
        var role = Request.Headers["X-User-Role"].FirstOrDefault() ?? "User";

        var claims = new[] { new Claim(ClaimTypes.Name, name), new Claim(ClaimTypes.Role, role) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
