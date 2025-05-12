using FluentMigrator;

namespace RecipeBook.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER, "Create table Recipe")]
    public class Version0000001 : VersionBase
    {

        public override void Up()
        {

            CreateTable("Users")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable();
        }
    }
}
