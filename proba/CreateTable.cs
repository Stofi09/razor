using System.Data.SqlClient;
using Dapper;
namespace proba
{
    public class CreateTable
    {
        void CreateDatabaseTables(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var createTables = configuration.GetValue<bool>("DatabaseSettings:CreateTablesOnStartup");

            if (createTables)
            {
                var sqlScript = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "CreateTables.sql"));

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(sqlScript);
                }
            }
        }

    }
}
