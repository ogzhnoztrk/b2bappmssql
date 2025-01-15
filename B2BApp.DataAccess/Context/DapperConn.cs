using Dapper;
using Microsoft.Data.SqlClient;

namespace B2BApp.DataAccess.Context
{
    public class DapperConn
    {
        public static async Task<IEnumerable<T>> GetDataAsync<T>(string query, object paramaters = null)
        {
            try
            {
                using (var conn = new SqlConnection("Data Source=OGUZHAN-OZTURK\\SQLEXPRESS;Initial Catalog=b2bTest;Integrated Security=True;Connect Timeout=300;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"))
                {

                    return await conn.QueryAsync<T>(query, paramaters);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static IEnumerable<T> GetData<T>(string query, object paramaters = null)
        {
            using (var conn = new SqlConnection("Data Source=OGUZHAN-OZTURK\\SQLEXPRESS;Initial Catalog=b2bTest;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                return conn.Query<T>(query, paramaters);
            }
        }
       
    }
}
