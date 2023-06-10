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
            lock (_lockObject)
            {
                using (StreamWriter streamWriter = File.AppendText(_logFilePath))
                {
                    streamWriter.WriteLine($"{DateTime.Now}: {"sd"}");
                }
            }
        }

        public void Log(HttpRequest context)
        {
            throw new NotImplementedException();
        }

        public void Log( HttpResponse context)
        {
            throw new NotImplementedException();
        }
    }
}
