using EGNDotNetTrainingBatch4.RestApi.DataBase;
using EGNDotNetTrainingBatch4.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//IN PostMan, Get and Delete do not get Body support
namespace EGNDotNetTrainingBatch4.RestApi.Controllers
{
    [Route("api/[controller]")] //endPoint
    [ApiController]
    public class BlogController : ControllerBase
    {
        
         // HttpMethod = get=>readand edit, post=>create,put=>update, 
        private readonly AddDbContext _dbConnect; //put underscore(_) before variable which mean this is global variable
        public BlogController()
        {
            _dbConnect=new AddDbContext();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var item = _dbConnect.Blogs.ToList();
            return Ok(item);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _dbConnect.Blogs.Add(blog);
            var result = _dbConnect.SaveChanges();
            string message = result > 0 ? "Create Data Done" : "Create Data failed";
            return Ok(message);
        }
        [HttpPut("{id}")] // change the whole resources
        public IActionResult Update(int id,BlogModel blog)  
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No Data");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = _dbConnect.SaveChanges();
            string message = result > 0 ? "Update data Done" : "Update Data failed";
            return Ok(message);
        }
        [HttpPatch("{id}")] // change specific data
        public IActionResult Patch(int id,BlogModel blog)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No Data");
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            var result = _dbConnect.SaveChanges();
            string message = result > 0 ? "Update data Done" : "Update Data failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data");
            }
            _dbConnect.Blogs.Remove(item);
            var result = _dbConnect.SaveChanges();
            string message = result > 0 ? "Update data Done" : "Update Data failed";
            return Ok(message);
        } 
        
    }
}
