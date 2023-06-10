using Logging_v5.Logger.Services;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Logging_v5.Logger.Repositories
{
    public class DatabaseLog : ILogType
    {
        private readonly IConfiguration _configuration;
        public DatabaseLog(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Log(string message)
        {

            // define INSERT query with parameters
            string query = "INSERT INTO dbo.Log (LogInfo, Time, Message) " +
                           "VALUES (@LogInfo, @Time, @Message) ";

            // create connection and command
            using (SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("LogConnectionString")))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@LogInfo", SqlDbType.VarChar, 50).Value = "information";
                cmd.Parameters.Add("@Time", SqlDbType.VarChar, 50).Value = DateTime.Now;
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 50).Value = message;


                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
