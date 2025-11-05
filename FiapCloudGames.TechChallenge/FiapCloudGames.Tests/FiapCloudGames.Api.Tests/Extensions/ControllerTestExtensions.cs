using FiapCloudGames.Api.Controllers.v1;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace FiapCloudGames.Api.Tests.Extensions;

/// <summary>
/// Extensão auxiliar para simular Claims do usuário nos testes
/// </summary>
[ExcludeFromCodeCoverage]
public static class ControllerTestExtensions
{
    public static void SetUserClaims(this BibliotecaJogosController controller, Guid userId, string role)
    {
        controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
        };
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, role)
        };
        controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
    }
}