using Microsoft.Extensions.Configuration;
using RecipeBook.Domain.Enums;

namespace RecipeBook.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static DataBaseType DataBaseType(this IConfiguration configuration)
        {
            var databaseType = configuration.GetConnectionString("DatabaseType");

            return (DataBaseType)Enum.Parse(typeof(DataBaseType), databaseType);
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            var dbType = configuration.DataBaseType();
            if (dbType == Domain.Enums.DataBaseType.SqlServer)
            {
                return configuration.GetConnectionString("SqlServerConnection");
            }
            else if (dbType == Domain.Enums.DataBaseType.MySql)
            {
                return configuration.GetConnectionString("MySqlConnection");
            }
            return "";
        }
    }
}
