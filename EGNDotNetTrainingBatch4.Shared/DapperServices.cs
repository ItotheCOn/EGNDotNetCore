using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace EGNDotNetTrainingBatch4.Shared
{
    public class DapperServices
    {
        //Dapper Has only two Cases which are Execute and Query so we only need to think of it
        /*private readonly string _connectionString; // connectionstring can change anytime so we have to do manual
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
        public M QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = db.Query<M>(query, param).FirstOrDefault();
            return result;
        }*/
        private readonly string _dapperService;
        public DapperServices(string connection)
        {
            _dapperService = connection;
        }
        public List<M> Query<M>(string query)
        {
            using IDbConnection db = new SqlConnection(_dapperService);
            var item = db.Query<M>(query).ToList();
            return item;
        } 
        public M QueryFirstOrDefault<M>(string query,object? param = null)
        {
            using IDbConnection db = new SqlConnection(_dapperService);
            var item = db.Query<M>(query, param).FirstOrDefault();
            return item!;
        }
        public int Execute(string query,object? param = null)
        {
            using IDbConnection db = new SqlConnection(_dapperService);
            var item = db.Execute(query, param);
            return item;
        }

    }
}
