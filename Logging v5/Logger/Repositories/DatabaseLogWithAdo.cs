using Logging_v5.Logger.Services;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Logging_v5.Logger.Repositories
{
    public class DatabaseLogWithAdo 
    {
        private readonly IConfiguration _configuration;
        string createDatabaseQuery = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Logs') " +
                            "CREATE DATABASE Logs";

        string createTableQuery = "USE Logs " + "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Log' AND TABLE_SCHEMA='dbo') " +
                                  "CREATE TABLE Log (LogType NVARCHAR(50) ,StatusCode NVARCHAR(50),Time NVARCHAR(50) ,Type NVARCHAR(50) ,Message NVARCHAR(500),UserAgent NVARCHAR(300))";

        string query = "INSERT INTO dbo.Log (LogType,StatusCode,Time,Type, Message,UserAgent) " +
           "VALUES (@LogType,@StatusCode,@Time,@Type,@Message,@UserAgent) ";

        public DatabaseLogWithAdo(IConfiguration configuration)
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
