using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Console.Controllers
{
    [AuthorizationOperationGroup(Name = "系统入口")]
    public class HomeController : WebBaseController
    {
        // GET: /<controller>/
        [AuthorizationOperation(Name = "首页")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
