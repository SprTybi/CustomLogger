using Logging_v5.Logger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace Logging_v5.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ICalculateDataTime _calculate;
        public HomeController(ICalculateDataTime calculate)
        {
            _calculate = calculate;
        }

        [HttpGet("{date}")]
        public ActionResult<int> ToPersianDate(DateTime date)
        {
            var time = _calculate.ToPersian(date);
            return time.DayOfYear;
        }


        [HttpGet("{EnterPersianDate}")]
        public ActionResult<int> GetDayOfYear(DateTime EnterPersianDate)
        {
            return _calculate.CalculateGregorianDay(EnterPersianDate);
        }

    }
        
}


