using FiapCloudGames.Domain.Repositories.v1;
using FiapCloudGames.Infrastructure.Repositories.v1;
using FiapCloudGames.Infrastructure.Seed;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services)
    {
        services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

        services.AddHostedService<DataSeederHostedService>();

        return services;
    }
}