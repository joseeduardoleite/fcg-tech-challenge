using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure.Auth;

[ExcludeFromCodeCoverage]
public record JwtSettings(
    string Issuer = "",
    string Audience = "",
    string SecretKey = "",
    double ExpirationMinutes = 60.0
);