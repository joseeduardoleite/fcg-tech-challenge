using FiapCloudGames.Application.Mappings;
using FiapCloudGames.Application.Services.v1;
using FiapCloudGames.Domain.Services.v1;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IJogoService, JogoService>();
        services.AddScoped<IBibliotecaJogoService, BibliotecaJogoService>();

        services.AddAutoMapper(mapperConfigurationExpression =>
        {
            mapperConfigurationExpression.AddProfile(typeof(UsuarioMapping));
            mapperConfigurationExpression.AddProfile(typeof(JogoMapping));
            mapperConfigurationExpression.AddProfile(typeof(BibliotecaJogoMapping));
        });

        return services;
    }
}