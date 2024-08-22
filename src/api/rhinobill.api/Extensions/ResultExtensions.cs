using rhinobill.core.Models.Results;

namespace rhinobill.api.Extensions
{
    public static class ResultExtensions
    {
        public static void ThrowIfFailed<T>(this Result<T> result)
        {
            if (!result.IsSuccess)
                throw new ResultException(result.Error);
        }

    }
}
