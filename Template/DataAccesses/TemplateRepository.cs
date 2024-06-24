using System.Data;
using System.Data.SqlClient;

namespace Sample_MinimalAPI.DataAccesses
{
    public class TemplateRepository(IConfiguration configuration) : IRepository
    {
        //TODO 修改連線字串
        //透過Configuration取得連線字串
        public string ConnectionString => configuration["DBConnection:Default"];

        //透過ConnectionString取得連線
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        public string GetSqlVersion()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT @@VERSION";
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}