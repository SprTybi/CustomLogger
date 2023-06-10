using System;
using System.Globalization;

namespace Logging_v5.Logger.Services
{
    public interface ICalculateDataTime
    {
        DateTime ToPersian(DateTime date);
        int CalculateGregorianDay(DateTime date);
    }
}
