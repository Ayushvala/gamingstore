using System.Data;
using Microsoft.Data.SqlClient;

namespace LearnDapper.DapperContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly String connectionString;
        //access cs in aps.js
        public DapperDbContext(IConfiguration configration)
        {
            _configuration = configration;
            connectionString = _configuration.GetConnectionString("Database");
        }
        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
