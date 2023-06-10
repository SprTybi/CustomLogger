using Logging_v5.Logger.Services;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Logging_v5.Logger.Repositories
{
    public class FileLog : ILogType
    {
        private static readonly object _lockObject = new object();
        private static readonly string _logFilePath = "logs/myapp.txt";
        public void Log(HttpContext context)
        {
            @LogType
            @Time
            @Type
            @UserAgent
            @StatusCode
            @Message
        }

        public void Log(HttpRequest context)
        {
            using (StreamWriter streamWriter = File.AppendText(_logFilePath))
            {
                var Type = context.Body.CanRead ? "Request" : "Response";
                streamWriter.WriteLine($" LogType:info   | Time:{DateTime.Now}   | Type:{Type}   | User-Agent:{context.Headers["User-Agent"]}   | StatusCode:200   | Message:{context.Path}");
            }
        }

        public void Log( HttpResponse context)
        {
            using (StreamWriter streamWriter = File.AppendText(_logFilePath))
            {
                streamWriter.WriteLine($"{DateTime.Now}: {"sd"}");
            }
        }
    }
}
