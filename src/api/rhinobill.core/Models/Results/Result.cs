using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Models.Results
{
    public class Result<T>
    {
        public T Data { get; set; }
        public ErrorResult Error { get; set; }
        public bool IsSuccess => Error == null;

        public Result(T data)
        {
            Data = data;
        }

        public Result(ErrorResult error)
        {
            Error = error;
        }

        public static implicit operator Result<T>(T data) => new Result<T>(data);
        public static implicit operator Result<T>(ErrorResult error) => new Result<T>(error);
    }
}
