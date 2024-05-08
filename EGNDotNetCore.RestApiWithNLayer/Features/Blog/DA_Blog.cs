using EGNDotNetCore.RestApiWithNLayer.Database;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata;
namespace EGNDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        //dataaccess
        private readonly AddDbContext _dbConnect;
        public DA_Blog()
        {
            _dbConnect = new AddDbContext();
        }

        public List<BlogModel> getBlog()
        {
            var item = _dbConnect.Blogs.ToList();
            return item;
        }

        public BlogModel getBlogById(int id)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }

        public int CreateBlog(BlogModel blogs)
        {
            _dbConnect.Blogs.Add(blogs);
            int data = _dbConnect.SaveChanges();
            return data;
        }

        public int UpdateBlog(int id,BlogModel blogs)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return 0;
            }
            item.BlogTitle = blogs.BlogTitle;
            item.BlogAuthor = blogs.BlogAuthor;
            item.BlogContent = blogs.BlogContent;
            int result = _dbConnect.SaveChanges();
            return result;
        }
        public int PatchBlog(int id, BlogModel blogs)
        {
            var item = getBlogById(id);
            if(item is null)
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(blogs.BlogTitle))
            {
                item.BlogTitle = blogs.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blogs.BlogAuthor))
            {
                item.BlogAuthor = blogs.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blogs.BlogContent))
            {
                item.BlogContent = blogs.BlogContent;
            }
            var result = _dbConnect.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var item = _dbConnect.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return 0;
            }
           _dbConnect.Blogs.Remove(item);
            var data = _dbConnect.SaveChanges();
            return data;
            
        }
    }
}
   
