using Microsoft.AspNetCore.Http.HttpResults;

namespace EGNDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;
        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }
        public List<BlogModel> getAllBlog()
        {
            var item = _daBlog.getBlog();
            return item;
        }
        public BlogModel getBlogById(int id)
        {
            var data = _daBlog.getBlogById(id);
            return data;
        }
        public int CreateBlog(BlogModel blogs)
        {
            var data = _daBlog.CreateBlog(blogs);
            return data;
        }
        public int UpdateBlog(int id,BlogModel blogs)
        {
            var item = _daBlog.UpdateBlog(id, blogs);
            return item;
        }
        public int Patch(int id,BlogModel blogs)
        {
            var item = _daBlog.PatchBlog(id, blogs);
            return item;
        }
        public int Deleteblog(int id)
        {
            var item = _daBlog.Delete(id);
            return item;
        }
    }
}
