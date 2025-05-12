using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Enums;
using RecipeBook.Domain.Interfaces;
using RecipeBook.Domain.Interfaces.User;
using RecipeBook.Infrastructure.DataAccess;
using RecipeBook.Infrastructure.DataAccess.Repositories;
using RecipeBook.Infrastructure.Extensions;
using System.Reflection;

namespace RecipeBook.Infrastructure
{
    public static class DependencyIngectionExtensionIF
    {
        public static void AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration.DataBaseType();

            if (dbType == DataBaseType.SqlServer)
            {
                AddDbContext_SQLServe(services, configuration);
                AddFluentMigrator_SqlServer(services, configuration);
            }
            else if (dbType == DataBaseType.MySql)
            {
                AddDbContext_MYSql(services, configuration);
                AddFluentMigrator_MySql(services, configuration);
            }
            AddRepositories(services);
        }

        private static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<RecipeBookDbContext>();
        }

        private static void AddDbContext_SQLServe(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.ConnectionString();
            services.AddDbContext<RecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddDbContext_MYSql(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.ConnectionString();
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<RecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserReadOnlyInterface, UserRepository>();
            services.AddScoped<IUserWriteOnlyInterface, UserRepository>();
            services.AddScoped<IDataBasePersist, DataBasePersist>();
        }

        private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.ConnectionString())
                .ScanIn(Assembly.Load("RecipeBook.Infrastructure")).For.All();
            });
        }

        private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddMySql5()
                .WithGlobalConnectionString(configuration.ConnectionString())
                .ScanIn(Assembly.Load("RecipeBook.Infrastructure")).For.All();
            });
        }
    }
}


