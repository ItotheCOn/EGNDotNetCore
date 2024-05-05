using Dapper;
using EGNDotNetTrainingBatch4.RestApi.Models;
using EGNDotNetTrainingBatch4.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EGNDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperServices _dapperServices = new DapperServices(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult Read()
        {
            string query = "SELECT * FROM Tbl_blog";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            //List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            var lst = _dapperServices.Query<BlogModel>(query);
            return Ok(lst);  
        }
        //Have to Write query before connection to database
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
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blogs);
            string message = result > 0 ? "Create data done" : "Create Data failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,BlogModel blogs)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No Data");
            }
            blogs.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blogs);
            string message = result > 0 ? "Update done" : "Update failed";
            return Ok(message);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data");
            }
            return Ok(item);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id,BlogModel blogs)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No Data");
            }
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
            if(condition.Length == 0)
            {
                return NotFound("No Data");
            }
            blogs.BlogId = id;
            condition = condition.Substring(0,condition.Length - 2);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {condition}
 WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int data = db.Execute(query, blogs);
            string message = data > 0 ? "Patch Done" : "patch failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = FindById(id);
            if(item is null)
            {
                return NotFound("No Data");
            }
            string query = "DELETE FROM Tbl_blog WHERE BlogId=@BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            int data = db.Execute(query,new BlogModel { BlogId =id});
            string message = data > 0 ? "Delete Done" : "Delete Failed";
            return Ok(message);
        }
        private BlogModel? FindById(int id)
        {
            string query = "select * from Tbl_blog where BlogId=@BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var result = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return result;
        }
    }
}
