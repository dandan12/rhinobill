using Microsoft.AspNetCore.Mvc;
using rhinobill.api.Extensions;
using rhinobill.core.Models.Results;

namespace rhinobill.api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public T GetResult<T>(Result<T> result)
        {
            result.ThrowIfFailed();

            return result.Data;
        }
    }
}
