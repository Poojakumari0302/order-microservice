# [< Back](../README.md) | API Setup

The API template is used to help OI engineers to quickly scaffold a API service.

Some features:

- The template is using DDD (Domain Driven Design)
- The template code base is **dotnet core version 3**
- The template uses EntityFramework Core
- The template uses **Docker** for containerisation and local environment orchestration
- The template uses Docker to create a **Postgres** database instance

## Requirements

Please ensure that the dotnet core SDK is installed locally in order to use the CLI.

The standard api template is installed into the existing `dotnet core` CLI.

## Api Template

The template is used to generate a new Demo web API solution.

### Api Solution Structure

- data
  - Migrations
- setup
  - docker-compose.yml (*optional* if the database, queue and network isn't running in docker - infrastructure)
- src
  - Api
  - Domain
  - Infrastructure
- tests
  - Domain.Tests
  - Integration

The above folder structure will replace {Stores} with the provided `-n` argument via the `dotnet new` CLI.

**Example(s):**

```vim
dotnet new oiapi -n Restaurants -un 5005 -s 5055 -c restaurant-api
```

The above will output the solution containing the following folder structure:

- data
  - Migrations
- src
  - Api
  - Domain
  - Infrastructure
- tests
  - Domain.Tests
  - Integration

```vim
dotnet new oiapi -n Restaurants -un 5005 -s 5055 -c restaurant-api
```

The above will scaffold the API and set the `-un` and `-s` ports to run on `5005` and `5055` respectively.

And with the `-c` parameter, it sets the container name to `restaurant-api`.

The namespace of the above solution will be - `OI.Restaurants.*`.

## TODO: Scripts

Need to create an automation script that will pull source code (we do not have a nuget repository) in order to install the template locally, so that the new template is available in the dotnet core CLI.

## Manual Installation

1. Pull repository into any folder location. Copy folder location, eg. `c:\MyDotnetCoreTemplates\CustomTemplates\Api`.

2. Open Command Prompt, Powershell or Terminal and execute `dotnet new -i {FolderLocation}`.

3. From command prompt, etc, execute `dotnet new -l` in order to view newly installed template.

> ### THIS FILE CAN BE DELETED UPON SERVICE GENERATION
