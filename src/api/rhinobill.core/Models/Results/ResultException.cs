using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Models.Results
{
    public class ResultException : Exception
    {
        public ResultException(ErrorResult error)
        {
            Error = error;
        }

        public ErrorResult Error { get; }
    }
}
