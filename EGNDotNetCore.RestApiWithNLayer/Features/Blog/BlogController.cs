using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EGNDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new BL_Blog();
        }
        [HttpGet]
        public IActionResult GetBlog()
        {
            var item =_blBlog.getAllBlog();
            return Ok(item);
        }
        [HttpGet("{id}")]
        public IActionResult getBlogById(int id)
        {
            var item = _blBlog.getBlogById(id);
            if(item is null)
            {
                return NotFound("No data");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blogs)
        {
            var item = _blBlog.CreateBlog(blogs);
            string message = item > 0 ? "Create Done" : "Create Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blogs)
        {
            var item = _blBlog.UpdateBlog(id, blogs);
            string message = item > 0 ? "Update data done" : "Update data failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult EditBlog(int id,BlogModel blogs)
        {
            var item = _blBlog.Patch(id, blogs);
            string message = item > 0 ? "Patch done" : "Patch failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Deleteblog(int id)
        {
            var item = _blBlog.Deleteblog(id);
            string message = item > 0 ? "Delete done" : "Delete failed";
            return Ok(message);
        }
    }
    
}
