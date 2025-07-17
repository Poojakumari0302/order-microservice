# [< Back](../README.md) | Migrations - Database schema

The API template contains its own migrations so that it manages its own persistance schemas.

## Manual Migration Execution

In order to get your API running without any schema issues, you will need to have a running instance of `PostgreSQL` running locally.

If you do not have a local instance running...

**Execute the following docker command from the root `/` folder, in a terminal:**

```csharp
docker-compose -f "setup/docker-compose.yml" up -d --build
```

When the instance is running successfully, navigate to the `Migrations` directory from the command line and execute the following command:

```shell
dotnet run
```

If any of the migrations were successfully executed, you will see all the output in the command line console.

## Troubleshooting Problems

### Migrations failing

---

> Error connecting to server

Please check the **connection string** in the migrations project. Ensure you're able to connect to the instance with the same credentials and details.
