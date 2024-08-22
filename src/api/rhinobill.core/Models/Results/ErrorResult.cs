using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Models.Results
{
    public record ErrorResult(string Code, string Message, ErrorType Type)
    {
        public static ErrorResult NotFound => new ErrorResult("NotFound", "The requested resource was not found", ErrorType.NotFound);
    }
}
