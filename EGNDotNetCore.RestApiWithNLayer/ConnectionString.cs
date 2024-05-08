using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetCore.RestApiWithNLayer
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-PKCNU0F\\MSSSQLSERVER",
            InitialCatalog = "DotNetTrainingbatch4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
        };


    }
}
