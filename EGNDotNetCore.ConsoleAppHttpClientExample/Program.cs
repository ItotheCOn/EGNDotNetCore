// See https://aka.ms/new-console-template for more information
using EGNDotNetCore.ConsoleAppHttpClientExample;
using Newtonsoft.Json;

//Console.WriteLine("Hello, World!");
//HttpClient client = new HttpClient();
//var response=await client.GetAsync("http://localhost:5084/api/blog");
//if (response.IsSuccessStatusCode)
//{
//    string JsonStr = await response.Content.ReadAsStringAsync();
//    //Console.WriteLine(JsonStr);
//    List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(JsonStr)!;
//    foreach(var item in lst)
//    {
//        //Console.WriteLine(JsonConvert.SerializeObject(item));
//        Console.WriteLine($"Id = {item.BlogId}");
//        Console.WriteLine($"Title = {item.BlogTitle}");
//        Console.WriteLine($"Author = {item.BlogAuthor}");
//        Console.WriteLine($"Content = {item.BlogContent}");
//    }  
//}
//
HttpClientExample _example = new HttpClientExample();
await _example.RunAsync();
