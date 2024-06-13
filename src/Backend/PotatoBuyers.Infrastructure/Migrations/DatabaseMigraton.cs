using Dapper;
using MySqlConnector;

namespace PotatoBuyers.Infrastructure.Migrations
{
    public static class DatabaseMigraton
    {
        public static void Migrate(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

            if (!records.Any()) 
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
        }
    }
}
