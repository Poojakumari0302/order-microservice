using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Configuration.Swagger;

/// <summary>
///     Configures the Swagger generation options.
/// </summary>
/// <remarks>This allows API versioning to define a Swagger document per API version after the
/// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
/// <remarks>
///     Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
/// </remarks>
/// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider = provider;

    /// <inheritdoc />
    public void Configure( SwaggerGenOptions options )
    {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach ( var description in provider.ApiVersionDescriptions )
        {
            options.SwaggerDoc( description.GroupName, CreateInfoForApiVersion( description ) );
        }
    }

    static OpenApiInfo CreateInfoForApiVersion( ApiVersionDescription description )
    {
        var info = new OpenApiInfo()
        {
            Title = "Orders API",
            Version = description.ApiVersion.ToString(),
            Description = "A RESTful Orders API with versioning.",
            Contact = new OpenApiContact() { Name = "Demo Engineering" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri( "https://opensource.org/licenses/MIT" ) }
        };

        if ( description.IsDeprecated )
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
