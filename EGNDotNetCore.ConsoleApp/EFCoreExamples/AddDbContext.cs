using EGNDotNetCore.ConsoleApp.Dtos;
using EGNDotNetCore.ConsoleApp.services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AddDbContext : DbContext   // connect c# with database
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //type of connection to connect
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString); //usesql server mean using microsoftsqlserver
        }

        public DbSet<BlogDto> Blogs { get; set; } // mean this c#object is equal to the table in database, we have to do */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<BlogDto> Blogs { get; set; }
    }
}
