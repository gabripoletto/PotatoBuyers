using FluentMigrator;

namespace PotatoBuyers.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_USER, "Create table to save the user's information")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("users")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Password").AsString(5000).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Cpf").AsString(14).NotNullable()
                .WithColumn("Telephone").AsString(15).NotNullable();
        }
    }
}
