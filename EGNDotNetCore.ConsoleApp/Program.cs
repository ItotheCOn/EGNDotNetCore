// See https://aka.ms/new-console-template for more information
using EGNDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
//ctrl+ =suggestion

Console.WriteLine("Hello, World!");
AdoDotNet adoDotNet = new AdoDotNet();
//adoDotNet.Read();
adoDotNet.Create("Test title2", "Test author2", "Test content2");

AssemblyDescriptionAttribute;