using FiapCloudGames.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Api.Examples;

[ExcludeFromCodeCoverage]
public sealed class UsuarioLoginRequestExample : IExamplesProvider<UsuarioLoginDto>
{
    public UsuarioLoginDto GetExamples() => new(
        Email: "jose.fgc@gmail.com",
        Senha: "Jose@1234"
    );
}