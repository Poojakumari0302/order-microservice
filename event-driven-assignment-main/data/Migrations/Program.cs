using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrations._2025._02;
using System;
using System.IO;

IConfiguration _configuration = null;

Console.WriteLine("Configuration migration startup.");
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Console.WriteLine("> Configuration migration for environment: {0}.", environmentName);

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("migrations.appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"migrations.appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
_configuration = builder.Build();

Console.WriteLine("> ConnectionString to be used: {0}", _configuration.GetConnectionString("default"));

var serviceProvider = CreateServices();

using (var scope = serviceProvider.CreateScope())
{
    UpdateDatabase(scope.ServiceProvider);
}

Console.WriteLine("Migrations completed.");

IServiceProvider CreateServices()
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb =>
            rb.AddPostgres()
            .WithGlobalConnectionString(_configuration.GetConnectionString("default"))
            .ScanIn(typeof(Migration202502250950).Assembly).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider();
}

void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    if (runner.HasMigrationsToApplyUp())
        runner.MigrateUp();
    else
        Console.WriteLine("No migrations to run.");
}
