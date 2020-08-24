using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
