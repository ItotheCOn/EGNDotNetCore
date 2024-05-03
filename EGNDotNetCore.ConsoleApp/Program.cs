// See https://aka.ms/new-console-template for more information
using EGNDotNetCore.ConsoleApp.AdoDotNetExamples;
using EGNDotNetCore.ConsoleApp.DapperExamples;
using EGNDotNetCore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
//ctrl+ =suggestion

Console.WriteLine("Hello, World!");
//AdoDotNet adoDotNet = new AdoDotNet();
//adoDotNet.Create("Test title114", "Test author114", "Test content114");
//adoDotNet.Read();
//adoDotNet.Update(2004, "Test title22", "Test author2", "Test content");
//adoDotNet.Delete(2004);
//adoDotNet.Edit(2005);
DapperExample dapper = new DapperExample();
dapper.Run();
//EFCoreExample efCoreExample = new EFCoreExample();
//efCoreExample.Run();
Console.ReadKey();

