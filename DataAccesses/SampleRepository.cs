using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_MinimalAPI.DataAccesses
{
    public class SampleRepository(IConfiguration configuration) : IRepository
    {
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