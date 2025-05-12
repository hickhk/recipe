using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using RecipeBook.Domain.Enums;

namespace RecipeBook.Infrastructure.Migrations
{
    public static class DataBaseMigrations
    {
        public static void Migrate(DataBaseType dataBaseType, string connectionString, IServiceProvider serviceProvider)
        {
            if (dataBaseType == DataBaseType.SqlServer)
            {
                EnsureDatabaseCreated_SqlServer(connectionString);
            }
            else if (dataBaseType == DataBaseType.MySql)
            {
                EnsureDatabaseCreated_MySql(connectionString);
            }
            MigrationDatabase(serviceProvider);
        }

        private static void EnsureDatabaseCreated_SqlServer(string connectionString)
        {
            var parameters = new DynamicParameters();
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.Remove("Database");

            parameters.Add("databaseName", databaseName);

            using var dbConnection = new SqlConnection(connectionStringBuilder.ConnectionString);
            var records = dbConnection.Query($"SELECT * FROM SYS.DATABASES WHERE NAME = @databaseName", new { databaseName });

            if (!records.Any())
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void EnsureDatabaseCreated_MySql(string connectionString)
        {
            var parameters = new DynamicParameters();
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            parameters.Add("databaseName", databaseName);

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            var records = dbConnection.Query($"SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @databaseName", new { databaseName });

            if (!records.Any())
            {
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();

        }
    }
}
