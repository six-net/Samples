using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EZNEW.Model;

namespace Api.Console.Filters
{
    /// <summary>
    /// Api exception filter
    /// </summary>
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(Result.FailedResult(context.Exception));
        }
    }
}
