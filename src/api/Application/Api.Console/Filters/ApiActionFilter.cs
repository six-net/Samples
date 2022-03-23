using EZNEW.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Filters
{
    public class ApiActionFilter : IActionFilter, IResultFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult && (objectResult.StatusCode == null || objectResult.StatusCode == StatusCodes.Status200OK))
            {
                var resultValue = objectResult.Value;
                if (resultValue is not IResult)
                {
                    context.Result = new JsonResult(Result.SuccessResult(data: resultValue));
                }
            }
        }
    }
}
