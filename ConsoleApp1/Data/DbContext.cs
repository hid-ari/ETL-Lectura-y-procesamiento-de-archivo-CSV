using Microsoft.Data.SqlClient;

namespace Data
{
    public class DbContext
    {
        private readonly string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=MercaJumbo;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}