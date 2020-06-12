using EZNEW.Web.Mvc;
using EZNEW.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Console.Filters
{
    /// <summary>
    /// console exception filter
    /// </summary>
    public class ConsoleExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var isAjax = context.HttpContext.Request.IsAjaxRequest();
            if (isAjax)
            {
                context.Result = new JsonResult(AjaxResult.FailedResult(context.Exception));
            }
            else
            {
                context.Result = new RedirectToActionResult("error", "home", null);
            }
        }
    }
}
