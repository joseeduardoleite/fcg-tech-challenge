using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Api.Utils;

[ExcludeFromCodeCoverage]
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
        => provider.ApiVersionDescriptions.ToList()
            .ForEach(description => options.SwaggerDoc(
                name: description.GroupName,
                info: new OpenApiInfo
                {
                    Title = $"FIAP Cloud Games API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                }
            )
        );
}