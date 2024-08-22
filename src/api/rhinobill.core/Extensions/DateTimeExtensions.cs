using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime datetime)
        {
            return new DateOnly(datetime.Year, datetime.Month, datetime.Day);
        }
    }
}
