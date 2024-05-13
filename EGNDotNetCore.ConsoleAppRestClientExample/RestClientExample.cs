using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EGNDotNetCore.ConsoleAppRestClientExample
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7298"));
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
            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr =  response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(JsonStr)!;
                foreach (var item in lst)
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
            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string JsonStr =  response.Content!;
                var item = JsonConvert.DeserializeObject<BlogModel>(JsonStr)!;
                Console.WriteLine($"id = {item.BlogId}");
                Console.WriteLine($"Title = {item.BlogTitle}");
                Console.WriteLine($"Author = {item.BlogAuthor}");
                Console.WriteLine($"Content = {item.BlogContent}");
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blogs = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson = JsonConvert.SerializeObject(blogs);
            // HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            //var response = await _client.PostAsync(blogJson, httpContent);
            var restRequest = new RestRequest(_blogEndpoint,Method.Post);
            var response = await _client.ExecuteAsync(restRequest);
            restRequest.AddJsonBody(blogs);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task UpdateAync(int id, string title, string author, string content)
        {
            BlogModel blogs = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            //string JsonStr = JsonConvert.SerializeObject(blogs);
            //HttpContent httpContent = new StringContent(JsonStr, Encoding.UTF8, Application.Json);
            //var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            restRequest.AddJsonBody(blogs);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task PatchAsync(int id, string title, string author, string content)
        {
            BlogModel blogs = new BlogModel();
            
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
            blogs.BlogId = id;
            //string JsonStr = JsonConvert.SerializeObject(blogs);
            //HttpContent httpContent = new StringContent(JsonStr, Encoding.UTF8, Application.Json);
            //var response = await _client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Patch);
            var response = await _client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        private async Task DeleteBlogAsync(int id)
        {
            var restrequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restrequest);
            if (response.IsSuccessStatusCode)
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message =  response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
