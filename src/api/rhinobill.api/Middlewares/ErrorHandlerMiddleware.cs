using rhinobill.core.Models.Results;
using System.Net;

namespace rhinobill.api.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(ResultException ex)
            {
                // do logging
                var error = ex.Error;

                context.Response.StatusCode = (int)error.Type;
                await context.Response.WriteAsJsonAsync(error);
            }
            catch(Exception ex)
            {
                // do logging
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new ErrorResult("InternalServerError", "An error has occurred.", ErrorType.InternalServerError);
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
