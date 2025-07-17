using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Api.Configuration.Middleware;
using Api.Configuration.ServiceCollection;
using Api.Configuration.Swagger;
using Domain.Shared.Settings;
using Infrastructure;
using Serilog;
using Serilog.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;
using Serilog.Events;
using Microsoft.AspNetCore.Http;
using Domain.OrderAggregate.Events;
using Infrastructure.Kafka;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        EnvironmentName = environment?.EnvironmentName;
        IsDevelopment = environment?.IsDevelopment() ?? false;

        Configuration = configuration;

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .CreateLogger();
    }


    public IConfiguration Configuration { get; }
    public string EnvironmentName { get; }
    public bool IsDevelopment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc()
            .AddApplicationPart(typeof(Startup).Assembly);

        services.Configure<AuthorisationSettings>(Configuration.GetSection("AuthorisationSettings"));
        services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
        services.Configure<MailProviderSettings>(Configuration.GetSection("MailProviderSettings"));
        services.Configure<CacheSettings>(Configuration.GetSection("CacheSettings"));

        services.AddApiVersioning(
            options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });
        services.AddVersionedApiExplorer(
            options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                // integrate xml comments
                options.IncludeXmlComments(XmlCommentsFilePath);
            });

        ConfigureDatabase(services);
        ConfigureDependencies(services);
        ConfigureServiceCommunication(services);
        ConfigureDistributedCaching(services);
        ConfigureHealthChecks(services);

        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddSerilog();
        
        app.UseDeveloperExceptionPage();

        app.UseSerilogRequestLogging();
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseMiddleware<ApiLoggingMiddleware>();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            // build a swagger endpoint for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapGet("/", context => context.Response.WriteAsync("=^.^="));
        });
    }

    /// <summary>
    ///     Configure the API service's database and specify it's connection string.
    /// </summary>
    /// <param name="services"></param>
    public virtual void ConfigureDatabase(IServiceCollection services)
    {
        services
            .AddEntityFrameworkNpgsql()
            .AddDbContext<ApplicationDbContext>((serviceProvider, opt) => 
                opt.UseNpgsql(Configuration.GetConnectionString("default"))
                    .UseInternalServiceProvider(serviceProvider));
    }

    /// <summary>
    ///     Configure the API service's communication for it's respective environment.
    /// </summary>
    public virtual void ConfigureServiceCommunication(IServiceCollection services)
    {
        var topicCreator = new TopicCreator("localhost:9092");
        topicCreator.EnsureTopicExistsAsync("order-registered").Wait();
        
        services.AddMassTransit(x => {
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                // rider.AddConsumer<KafkaMessageConsumer>();
                rider.AddProducer<OrderRegisteredEvent>("order-registered");

                rider.UsingKafka((context, k) =>
                {
                    k.Host("localhost:9092");
                    
                    k.TopicEndpoint<OrderRegisteredEvent>("order-registered", "order-assignment-group", e =>
                    {
                        // e.ConfigureConsumer<KafkaMessageConsumer>(context);
                    });
                });
            });
        });
    }

    /// <summary>
    ///     Configure the API service's distributed caching.
    /// </summary>
    public virtual void ConfigureDistributedCaching(IServiceCollection services)
    {
        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = Configuration.GetValue<string>("CacheSettings:Server");
        });
    }

    /// <summary>
    ///     Configure the API service's dependencies required for application execution.
    /// </summary>
    public virtual void ConfigureDependencies(IServiceCollection services)
    {
        // AutoMapper.ServiceCollectionExtensions.AddAutoMapper(services, typeof(Startup));
        // services.AddApplicationDependencies();
        services.AddApplicationPipelineBehaviors();
        services.AddDomainDependencies();
        services.AddInfrastructureDependencies(Configuration);
    }

    /// <summary>
    ///     Configure the API service's health checks and map to a /health endpoint.
    /// </summary>
    public virtual void ConfigureHealthChecks(IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
    }

    /// <summary>
    ///     Get the XML comments' file path.
    /// </summary>
    static string XmlCommentsFilePath
    {
        get
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }
    }
}