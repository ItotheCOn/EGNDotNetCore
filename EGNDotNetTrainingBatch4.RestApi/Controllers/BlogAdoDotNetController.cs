using Dapper;
using EGNDotNetTrainingBatch4.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace EGNDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read()
        {
            string query = "select * from Tbl_blog";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            DataTable tb = new DataTable();
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            runQuery.Fill(tb);
            connection.Close();
            //List<BlogModel> lst = new List<BlogModel>();
            /*foreach(DataRow dr in tb.Rows)
            {
                //option 1
                BlogModel blogs = new BlogModel();
                blogs.BlogId = Convert.ToInt32(dr["BlogId"]);
                blogs.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                blogs.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                blogs.BlogContent = Convert.ToString(dr["BlogContent"]);
                //different option,option2
                BlogModel blogs = new BlogModel
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    BlogTitle = Convert.ToString(dr["BlogTitle"]),
                    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                    BlogContent = Convert.ToString(dr["BlogContent"])
            };
                lst.Add(blogs);
            }*/
            // dr is the same functiion as DataRow dr
            //option3
            List<BlogModel> lst = tb.AsEnumerable().Select(dr => new BlogModel {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult getBlogByid(int id)
        {
            string query = "select * from Tbl_blog where BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blogid", id);
            DataTable tb = new DataTable();
            SqlDataAdapter runQUery = new SqlDataAdapter(cmd);
            runQUery.Fill(tb);
            connection.Close();
            if(tb.Rows.Count == 0)
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
            return Ok(item);
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
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blogs.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogs.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogs.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
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
            string query = "select * from Tbl_blog where BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
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
            newQuery += condition + " WHERE BlogId=@BlogId"+";";
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
    }
}
