using EGNDotNetCore.ConsoleApp.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample
    {
        private readonly AddDbContext db = new AddDbContext();
        public void Run()
        {
            //Read();
            //Edit(2005);
            //Create("Title211", "Author211", "Content211");
            //Update(3003, "tt", "ta", "tc");
            // Delete(3003);
        }
        private void Read()
        {
            var data = db.Blogs.ToList(); // just writing c# code, it will automatically change to query string in behind
            foreach(BlogDto item in data)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.BlogAuthor);
            }
        }
        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (item is null)
            {
                Console.WriteLine("NO Data");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
        private void Create(string title,string author,string content)
        {
            var item = new BlogDto
            {
                BlogTitle =title,
                BlogAuthor =author,
                BlogContent =content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Create Data Done" : "Create data failed";
            Console.WriteLine(message);
        }
        private void Update(int id,string title,string author,string content)
        {
            var item = db.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if(item is null)
            {
                Console.WriteLine("No Data");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result=db.SaveChanges();
            string message = result > 0 ? "Update Data Done" : "Update Data failed";
            Console.WriteLine(message); 
        } 
        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No Data");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Delete Data done" : "Delete Data failed";
            Console.WriteLine(message);
        }
       
    }
}
