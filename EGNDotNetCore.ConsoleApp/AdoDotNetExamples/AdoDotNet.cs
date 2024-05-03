using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Data.Common;

namespace EGNDotNetCore.ConsoleApp.AdoDotNetExamples
{
    internal class AdoDotNet
    {
        /*
         private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
         {
             DataSource = "DESKTOP-PKCNU0F\\MSSSQLSERVER",
             InitialCatalog = "DotNetTrainingbatch4",
             UserID ="sa",
             Password ="sa@123"
         };
         public void ReadData()
         {
             SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
             connection.Open();
             Console.WriteLine("Connection open");
             string query = @"SELECT * FROM Tbl_blog";
             SqlCommand cmd = new SqlCommand(query, connection);
             SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
             DataTable tb = new DataTable();
             runQuery.Fill(tb);
             connection.Close();
             Console.WriteLine("Connection closed");
             foreach(DataRow dr in tb.Rows)
             {
                 Console.WriteLine("Id = " + dr["BlogId"]);
                 Console.WriteLine("Title = " + dr["BlogTitle"]);
                 Console.WriteLine("Author = " + dr["BlogAuthor"]);
                 Console.WriteLine("Content = " + dr["BlogContent"]);
             }
         }
         public void Edit(int id)
         {
             SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
             connection.Open();
             Console.WriteLine("Connection open");
             string query = "SELECT * FROM Tbl_blog WHERE BlogId=@BlogId";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@BlogId", id);
             SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
             DataTable tb = new DataTable();
             runQuery.Fill(tb);
             connection.Close();
             if(tb.Rows.Count == 0)
             {
                 Console.WriteLine("No Data");
                 return;
             }
             DataRow dr = tb.Rows[0];
             Console.WriteLine("Id = " + dr["BlogId"]);
             Console.WriteLine("Title = " + dr["BlogTitle"]);
             Console.WriteLine("Author = " + dr["BlogAuthor"]);
             Console.WriteLine("Content = " + dr["BlogContent"]);

         }
         public void Update(int id,string title,string author,string content)
         {
             SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
             connection.Open();
             Console.WriteLine("Connection Open");
             string query = @"UPDATE [dbo].[Tbl_blog]
    SET [BlogTitle] = @BlogTitle
       ,[BlogAuthor] = @BlogAuthor
       ,[BlogContent] = @BlogContent
  WHERE [BlogId] =@BlogId;";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@BlogId", id);
             cmd.Parameters.AddWithValue("@BlogTitle", title);
             cmd.Parameters.AddWithValue("@BlogAuthor", author);
             cmd.Parameters.AddWithValue("@BlogContent", content);
             int result = cmd.ExecuteNonQuery();
             connection.Close();
             Console.WriteLine("Connection closed");
             string message = result > 0 ? "Update successfully" : "Update failed";
         }
         public void Create(string title,string author,string content)
         {
             SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
             connection.Open();
             Console.WriteLine("Connection open");
             string query = @"
 INSERT INTO [dbo].[Tbl_blog]
            ([BlogTitle]
            ,[BlogAuthor]
            ,[BlogContent])
      VALUES
            (@BlogTitle
            ,@BlogAuthor
            ,@BlogContent) ";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@BlogTitle", title);
             cmd.Parameters.AddWithValue("@BlogAuthor", author);
             cmd.Parameters.AddWithValue("@BlogContent", content);
             int result = cmd.ExecuteNonQuery();
             connection.Close();
             Console.WriteLine("Connection closed");
             string message = result > 0 ? "Create Data successfully" : "Create data failed";
             Console.WriteLine(message);
         }
         public void Delete(int id)
         {
             SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
             connection.Open();
             Console.WriteLine("Connection open");
             string query = @"DELETE FROM [dbo].[Tbl_blog]
       WHERE [BlogId] =@BlogId;";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.Parameters.AddWithValue("@BlogId", id);
             int result = cmd.ExecuteNonQuery();
             connection.Close();
             Console.WriteLine("Connection closed");
             string message = result > 0 ? "Delete Data successfully" : "Delete Data failed";
             Console.WriteLine(message);
         } */
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-PKCNU0F\\MSSSQLSERVER",
            InitialCatalog = "DotNetTrainingbatch4",
            UserID ="sa",
            Password="sa@123"
        };
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = "select * from Tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable tb = new DataTable();
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            runQuery.Fill(tb);
            connection.Close();
            Console.WriteLine("Connection close");
            foreach(DataRow item in tb.Rows)
            {
                Console.WriteLine(item["BlogId"]);
                Console.WriteLine(item["BlogTitle"]);
                Console.WriteLine(item["BlogAuthor"]);
                Console.WriteLine(item["BlogContent"]);
            }
        }
        public void Edit(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection open");
            string query = @"select * from Tbl_blog where BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            DataTable tb = new DataTable();
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            runQuery.Fill(tb);
            connection.Close();
            Console.WriteLine("Connection closed");
            if(tb.Rows.Count == 0)
            {
                Console.WriteLine("NO Data");
                return;
            }
            DataRow item = tb.Rows[0];
            Console.WriteLine(item["BlogId"]);
            Console.WriteLine(item["BlogTitle"]);
            Console.WriteLine(item["BlogAuthor"]);
            Console.WriteLine(item["BlogContent"]);
        }
        public void Create(string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection open");
            string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Create data done" : "create data failed";
            Console.WriteLine(message);
        }
        public void Update(int id,string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("No Data");
            string query = @"UPDATE [dbo].[Tbl_blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update data done" : "Update data failed";
            Console.WriteLine(message);
        }
       public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");
            string query = @"
DELETE FROM [dbo].[Tbl_blog]
      WHERE [BlogId]=@BlogId;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete data done" : "Delete data failed";
            Console.WriteLine(message);
        }
    }
}
