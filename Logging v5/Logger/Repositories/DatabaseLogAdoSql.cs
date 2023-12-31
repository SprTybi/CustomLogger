﻿using Logging_v5.Logger.Services;
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
    public class DatabaseLogAdoSql : ILogType
    {
        private readonly IConfiguration _configuration;
        string createDatabaseQuery = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Logs') " +
                            "CREATE DATABASE Logs";

        string createTableQuery = "USE Logs " + "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Log' AND TABLE_SCHEMA='dbo') " +
                                  "CREATE TABLE Log (LogType NVARCHAR(50) ,StatusCode NVARCHAR(50),Time NVARCHAR(50) ,Type NVARCHAR(50) ,Message NVARCHAR(500),UserAgent NVARCHAR(300))";

        string query = "INSERT INTO dbo.Log (LogType,StatusCode,Time,Type, Message,UserAgent) " +
           "VALUES (@LogType,@StatusCode,@Time,@Type,@Message,@UserAgent) ";

        public DatabaseLogAdoSql(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public void Log(HttpRequest context)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("LogConnectionString")))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@LogType", SqlDbType.NVarChar, 50).Value = "info";
                        cmd.Parameters.Add("@Time", SqlDbType.NVarChar, 50).Value = DateTime.Now.ToString();
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = context.Body.CanRead ? "Request" : "Response";
                        cmd.Parameters.Add("@UserAgent", SqlDbType.NVarChar, 300).Value = context.Headers["User-Agent"].ToString();
                        cmd.Parameters.Add("@StatusCode", SqlDbType.NVarChar, 50).Value = "200";
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500).Value = context.Path.ToString();

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@LogType", SqlDbType.NVarChar, 50).Value = "wrn";
                        cmd.Parameters.Add("@Time", SqlDbType.NVarChar, 50).Value = DateTime.Now.ToString();
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = context.Body.CanRead ? "Request" : "Response";
                        cmd.Parameters.Add("@UserAgent", SqlDbType.NVarChar, 300).Value = context.Headers["User-Agent"].ToString();
                        cmd.Parameters.Add("@StatusCode", SqlDbType.NVarChar, 50).Value = "500";
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500).Value = ex.Message;

                        cmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Log(HttpResponse context)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("LogConnectionString")))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.Add("@LogType", SqlDbType.NVarChar, 50).Value = "info";
                        cmd.Parameters.Add("@Time", SqlDbType.NVarChar, 50).Value = DateTime.Now.ToString();
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = context.Body.CanRead ? "Request" : "Response";
                        cmd.Parameters.Add("@UserAgent", SqlDbType.NVarChar, 300).Value = context.Headers["User-Agent"].ToString();
                        cmd.Parameters.Add("@StatusCode", SqlDbType.NVarChar, 50).Value = context.StatusCode.ToString();
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 500).Value = "Result : " + RequestAndResponse(context);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public string RequestAndResponse(HttpResponse Response)
        {
            var body = string.Empty;
            Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(Response.Body, Encoding.UTF8, true, 1024, true);
            body = reader.ReadToEnd();
            Response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }

    }
}
