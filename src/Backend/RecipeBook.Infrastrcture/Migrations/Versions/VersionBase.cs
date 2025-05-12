using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
namespace RecipeBook.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
        {
            return Create.Table(tableName)
            .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn("Active").AsBoolean().NotNullable()
            .WithColumn("CreatedOn").AsDateTime2().NotNullable()
            .WithColumn("UpdatedOn").AsDateTime2().NotNullable();
        }
    }
}
