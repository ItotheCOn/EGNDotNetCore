using Dapper;
using EGNDotNetTrainingBatch4.RestApi.Models;
using EGNDotNetTrainingBatch4.Shared;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;

namespace EGNDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        
        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_blog";
            var lst = _adoDotNetService.Query<BlogModel>(query).ToList();
          

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult getBlogByid(int id)
        {
            string query = "select * from Tbl_blog where BlogId=@BlogId";

            /*
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            DataTable tb = new DataTable();
            SqlDataAdapter runQUery = new SqlDataAdapter(cmd);
            runQUery.Fill(tb);
            connection.Close();
            */
            /*
             This is the option 1
            AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            parameters[0] = new AdoDotNetParameter("@BlogId", id);
            var lst = _adoDotNetService.Query<BlogModel>(query, parameters);
            */
            //params
            var lst = _adoDotNetService.QueryFirstorDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            if(lst is null)
            {
                return NotFound("No data");
            }
            return Ok(lst);
            /*if(tb.Rows.Count == 0)
            {
                return NotFound("No Data");
            }
            DataRow dr = tb.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
            return Ok(item);*/
        }
        [HttpPost]
        public IActionResult Create(BlogModel blogs)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";
            /*SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blogs.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogs.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogs.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();*/
             int result=_adoDotNetService.Execute(query,
                 new AdoDotNetParameter("@BlogTitle",blogs.BlogTitle)
                , new AdoDotNetParameter("@BlogAuthor", blogs.BlogAuthor)
                , new AdoDotNetParameter("@BlogContent", blogs.BlogContent)
                );
            string message = result > 0 ? "Create done" : "Create failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,BlogModel blogs)
        {
            string query = @"UPDATE [dbo].[Tbl_blog]
    SET [BlogTitle] = @BlogTitle
       ,[BlogAuthor] = @BlogAuthor
       ,[BlogContent] = @BlogContent
  WHERE [BlogId] =@BlogId;";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blogs.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogs.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogs.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Update data done" : "Update failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blogs)
        {
            string query = "SELECT COUNT(*) FROM Tbl_blog WHERE BlogId=@BlogId";//count= return the number of rows that matched the specificed criterion.
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
//ExecuteScalar=method of sqlcommand in ADO.Net.
//ExecuteScalar= retrieve the count as a single value,
//int mean normally return set is object and null, so we have to cast the return set into data type that we want to change.
            int result = (int)cmd.ExecuteScalar();
            if (result == 0)
            {
                connection.Close();
                return NotFound("No Data");
            }
            string newQuery = " UPDATE Tbl_blog SET ";
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(blogs.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blogs.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blogs.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }
            if (condition.Length == 0)
            {
                return NotFound("No Data");
            }
            condition = condition.Substring(0,condition.Length - 2);
            newQuery += condition + " WHERE BlogId=@BlogId";
            SqlCommand newCmd = new SqlCommand(newQuery, connection);
            newCmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blogs.BlogTitle))
            {
                newCmd.Parameters.AddWithValue("@BlogTitle", blogs.BlogTitle);
            }

            if (!string.IsNullOrEmpty(blogs.BlogAuthor))
            {
                newCmd.Parameters.AddWithValue("@BlogAuthor", blogs.BlogAuthor);
            }

            if (!string.IsNullOrEmpty(blogs.BlogContent))
            {
                newCmd.Parameters.AddWithValue("@BlogContent", blogs.BlogContent);
            }

            int last = newCmd.ExecuteNonQuery();
            connection.Close();
            string message = last > 0 ? "Patch done" : "Patch failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = "DELETE FROM Tbl_blog WHERE BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Delete Done" : "Delete closed";
            return Ok(message);
        }
       
       
    }
}
