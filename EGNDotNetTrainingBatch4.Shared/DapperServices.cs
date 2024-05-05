using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace EGNDotNetTrainingBatch4.Shared
{
    public class DapperServices
    {
        private readonly string _connectionString; // connectionstring can change anytime so we have to do manual
        public DapperServices(string connection)
        {
            _connectionString = connection;
        }
        public List<T> Query<T>(string query)//this("Query")function is from DapperServices
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //This Query is from Dapper
            var lst = db.Query<T>(query).ToList();// Query<T> T is custm object,return or output base on that object
            return lst;
        }
        public int Execute(string query, Object? param = null)
        {
            // the original default value of param is null.
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Execute(query, param);
            return result;
        }
    }
}
