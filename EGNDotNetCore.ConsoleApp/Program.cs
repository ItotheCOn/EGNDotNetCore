// See https://aka.ms/new-console-template for more information
using EGNDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
//ctrl+ =suggestion

Console.WriteLine("Hello, World!");
AdoDotNet adoDotNet = new AdoDotNet();
adoDotNet.Create("Test title2", "Test author2", "Test content2");
adoDotNet.Read();
adoDotNet.Update(3, "Test title3", "Test author3", "Test content3");
adoDotNet.Delete(3);
adoDotNet.Edit(2);
//Console.ReadKey();

