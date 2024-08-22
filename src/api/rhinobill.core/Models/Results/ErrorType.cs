using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Models.Results
{
    public enum ErrorType
    {
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }
}
