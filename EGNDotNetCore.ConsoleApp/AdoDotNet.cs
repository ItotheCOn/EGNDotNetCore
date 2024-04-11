﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetCore.ConsoleApp
{
    internal class AdoDotNet
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder() {
                               //put the underscore before name that mean this variable is global variable
            DataSource = "DESKTOP-PKCNU0F\\MSSSQLSERVER",
            InitialCatalog = "DotNetTrainingbatch4",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection success");
            string query = "select * from Tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter runQuery = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            runQuery.Fill(tb);
            connection.Close();
            Console.WriteLine("Connection closed");
            foreach (DataRow dr in tb.Rows)
            {
                Console.WriteLine("Blog Id =>" + dr["BlogId"]);
                Console.WriteLine("Blog Title=>" + dr["BlogTitle"]);
                Console.WriteLine("Blog Author=>" + dr["BlogAuthor"]);
                Console.WriteLine("Blog Content=>" + dr["BlogContent"]);
            }

        }
        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection open");
            //string query = "INSERT INTO [dbo].[Tbl_blog]\r\n           ([BlogTitle]\r\n           ,[BlogAuthor]\r\n           ,[BlogContent])\r\n     VALUES\r\n           (@BlogTitle\r\n           ,@BlogAuthor\r\n           ,@BlogContent)"
            //\r\n mean nextline
            string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            
            int result=cmd.ExecuteNonQuery(); // return integer will be given

            connection.Close();
            string message = result > 0 ? "saving successful" : "Saving failed";
            Console.WriteLine(message);
        }
    }
}
