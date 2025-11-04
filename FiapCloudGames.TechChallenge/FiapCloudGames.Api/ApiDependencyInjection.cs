using FiapCloudGames.Api.AppServices.v1;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Api;

[ExcludeFromCodeCoverage]
public static class ApiDependencyInjection
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        services.AddScoped<IJogoAppService, JogoAppService>();

        return services;
    }
}