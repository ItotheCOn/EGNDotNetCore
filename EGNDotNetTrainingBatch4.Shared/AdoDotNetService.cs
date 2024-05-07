using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetTrainingBatch4.Shared
{
    public class AdoDotNetService
    {
        private readonly string _adoDotNetService;
        public AdoDotNetService(string connection)
        {
            _adoDotNetService = connection;
        }
        public List<M>Query<M>(string query, params AdoDotNetParameter[]? parameters )//params does not support default value
        {
            SqlConnection db = new SqlConnection(_adoDotNetService);
            db.Open();
            SqlCommand cmd = new SqlCommand(query,db);
            if(parameters is not null && parameters.Length > 0)
            {
                /*  foreach(var data in paramerters) //option 1->Easy way
                  {
                      cmd.Parameters.AddWithValue(data.Name, data.Value);
                  }
                */
                //cmd.Parameters.AddRange(paramerters.Select(data => new SqlParameter(data.Name, data.Value)).ToArray()); more difficult level
                var parametersValue = parameters.Select(data => new SqlParameter(data.Name, data.Value)).ToArray();
                cmd.Parameters.AddRange(parametersValue);
            }
            DataTable tb = new DataTable();
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            runQuery.Fill(tb);
            db.Close();
            string json = JsonConvert.SerializeObject(tb); //changing c#(dataTbale) into json
            var item = JsonConvert.DeserializeObject<List<M>>(json)!;
            return item;

        }
        public M QueryFirstorDefault<M>(string query, params AdoDotNetParameter[]? parameters)//params does not support default value
        {
            SqlConnection db = new SqlConnection(_adoDotNetService);
            db.Open();
            SqlCommand cmd = new SqlCommand(query, db);
            if (parameters is not null && parameters.Length > 0)
            {
                /*  foreach(var data in paramerters) //option 1->Easy way
                  {
                      cmd.Parameters.AddWithValue(data.Name, data.Value);
                  }
                */
                //cmd.Parameters.AddRange(paramerters.Select(data => new SqlParameter(data.Name, data.Value)).ToArray()); more difficult level
                var parametersValue = parameters.Select(data => new SqlParameter(data.Name, data.Value)).ToArray();
                cmd.Parameters.AddRange(parametersValue);
            }
            DataTable tb = new DataTable();
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            runQuery.Fill(tb);
            db.Close();
            string json = JsonConvert.SerializeObject(tb); //changing c#(dataTbale) into json
            var item = JsonConvert.DeserializeObject<List<M>>(json)!;
            return item[0];
        }
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection db = new SqlConnection(_adoDotNetService);
            db.Open();
            SqlCommand cmd = new SqlCommand(query, db);
            if(parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(data => new SqlParameter(data.Name, data.Value)).ToArray());
                
            }
            var result = cmd.ExecuteNonQuery();
            return result;
        }
    }
    public class AdoDotNetParameter
    {
        //nullable = 
        public AdoDotNetParameter()
        {

        }
        public AdoDotNetParameter(string name,object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
