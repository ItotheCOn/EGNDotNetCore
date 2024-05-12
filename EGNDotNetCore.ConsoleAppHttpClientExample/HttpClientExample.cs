using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EGNDotNetCore.ConsoleAppHttpClientExample
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7230") };
        private readonly string _blogEndpoint = "api/blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(2005);
            //await DeleteBlogAsync(6003);
            // await UpdateAync(2005, "TestAB", "TestBA", "TestCBA");
           // await PatchAsync(2005, "ddd", "", "");
        }
        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(JsonStr)!;
                foreach(var item in lst)
                {
                    Console.WriteLine($"Id = {item.BlogId}");
                    Console.WriteLine($"Title = {item.BlogTitle}");
                    Console.WriteLine($"Author = {item.BlogAuthor}");
                    Console.WriteLine($"Content = {item.BlogContent}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string JsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogModel>(JsonStr)!;
                Console.WriteLine($"id = {item.BlogId}");
                Console.WriteLine($"Title = {item.BlogTitle}");
                Console.WriteLine($"Author = {item.BlogAuthor}");
                Console.WriteLine($"Content = {item.BlogContent}");
            }
        }

        private async Task CreateAsync(string title,string author,string content)
        {
            BlogModel blogs = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson = JsonConvert.SerializeObject(blogs);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8,Application.Json);
            var response = await _client.PostAsync(blogJson, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        private async Task UpdateAync(int id,string title,string author,string content)
        {
            BlogModel blogs = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string JsonStr = JsonConvert.SerializeObject(blogs);
            HttpContent httpContent = new StringContent(JsonStr, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        private async Task PatchAsync(int id,string title,string author,string content)
        {
            BlogModel blogs = new BlogModel();
            blogs.BlogId = id;
            if (!string.IsNullOrEmpty(title))
            {
                blogs.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                blogs.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                blogs.BlogContent = content;
            }
            string JsonStr = JsonConvert.SerializeObject(blogs);
            HttpContent httpContent = new StringContent(JsonStr, Encoding.UTF8, Application.Json);
            var response = await _client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        private async Task DeleteBlogAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}
