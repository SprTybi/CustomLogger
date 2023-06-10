using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using static Logging_v5.Logger.Repositories.DatabaseLogWithAdo;

namespace Logging_v5.Logger.Services
{
    public interface ILogType
    {
        void Log(HttpRequest context);
        void Log(HttpResponse context);
    }
}
