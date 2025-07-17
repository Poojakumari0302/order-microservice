# Frontliners API

Project Description

This is an API I've worked on for a previous employer. It is based on an API template I had designed (including all markdown documentation and diagrams).

The solution structure tries to follow the DDD clean code structure seen in Microsoft's [eshop on containers](https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/introduce-eshoponcontainers-reference-app) (however, this version would be based on an outdated version).

For more information:

* [ARCHITECTURE.md](documentation/ARCHITECTURE.md) - high-level API service architecture details.
* [SETUP.md](documentation/SETUP.md) - Installation of the API into the `dotnet` CLI.
* [MIGRATIONS.md](documentation/MIGRATIONS.md) - Getting started with database schema migrations.
* [EVENTS.md](documentation/EVENTS.md) - Getting started and understanding events-based communication used.

---

## [Built With](#built-with)

* [Dotnet 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) - .NET - Free. Cross-platform. Open source.
* [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/) - Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.
* [PostgreSQL](https://www.postgresql.org/) - The World's Most Advanced Open Source Relational Database.
* [MassTransit](https://masstransit-project.com/) - A free, open-source distributed application framework for .NET.
* [Kafka](https://kafka.apache.org/) - Apache Kafka is a distributed event store and stream-processing platform.
* [xUnit](https://xunit.net/) - xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework.
* [App Metrics](https://www.app-metrics.io/) - App Metrics is an open-source and cross-platform .NET library used to record metrics within an application.
* [Swagger UI](https://swagger.io/tools/swagger-ui/) - Swagger UI allows anyone — be it your development team or your end consumers — to visualize and interact with the API’s resources without having any of the implementation logic in place.
* [Docker](https://www.docker.com/) - Package Software into Standardized Units for Development, Shipment and Deployment.
* [Fluent Migrator](https://fluentmigrator.github.io/) - Fluent Migrator is a migration framework for .NET much like Ruby on Rails Migrations.

---

## [Getting Started](#getting-started)

These instructions will get you a copy of the API up and running on your local machine for development and testing purposes. See deployment for notes on how this project is deployed.

### [Step 1 - Docker setup and startup (Optional)](#step-1)

> *If you have Postgres and RabbitMQ running inside docker on the demo-network network (locally), then you're able to skip the following.*

**Execute the following docker command from the root `/` folder, in a terminal:**

```csharp
docker-compose -f "setup/docker-compose.yml" up -d --build
```

If successful, you would've succesfully created and started the following containers:

* Db - a local instance of Postgresql database
* RabbmitMQ - a local instance of RabbitMQ
* Elasticsearch - a local instance of elasticsearch
* Kibana - a local instance of Kibana to visualise elastic data

**Execute the following docker command, in a terminal:**

```csharp
docker container ls
```

Should see running containers in your terminal:

```shell
CONTAINER ID        IMAGE                                                 COMMAND                  CREATED             STATUS              PORTS                                                                                        NAMES
550ecb54643e        postgres                                              "docker-entrypoint.s…"   57 seconds ago      Up 56 seconds       0.0.0.0:5432->5432/tcp                                                                       postgres
...
```

---

### [Step 2 - Data Migrations](#step-2)

> *Please note, you need to have installed the dotnet SDK before able to execute the following dotnet CLI command.*

Go to the `Migrations` directory in a terminal or command line tool.

**Execute the following command, in a terminal:**

```csharp
dotnet run
```

If successful, you should see the below output in terminal:

```shell
Configuration migration startup.
-------------------------------------------------------------------------------
VersionMigration migrating
-------------------------------------------------------------------------------
Beginning Transaction
CreateTable VersionInfo
Committing Transaction
VersionMigration migrated
-------------------------------------------------------------------------------
VersionUniqueMigration migrating
-------------------------------------------------------------------------------
Beginning Transaction
CreateIndex VersionInfo (Version)
AlterTable VersionInfo
CreateColumn VersionInfo AppliedOn DateTime
Committing Transaction
VersionUniqueMigration migrated
-------------------------------------------------------------------------------
VersionDescriptionMigration migrating
-------------------------------------------------------------------------------
Beginning Transaction
AlterTable VersionInfo
CreateColumn VersionInfo Description String
Committing Transaction
VersionDescriptionMigration migrated
-------------------------------------------------------------------------------
20191128064700: Migration20191128064700 migrating
-------------------------------------------------------------------------------
Beginning Transaction
CreateTable Demos
Committing Transaction
20191128064700: Migration20191128064700 migrated
Migrations completed.
```

---

### [Step 3 - Running the API](#step-2)

> *Please note, you need to have installed the dotnet SDK before able to execute the following dotnet CLI command.*

You are welcome to start up the application via terminal:

```shell
dotnet run
```

OR by starting up the application in your favorite IDE.

And browse to `http://localhost:5800/swagger/` in your favorite browser to view the API.

---

