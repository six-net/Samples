using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Console.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        IWebHostEnvironment env;

        public HomeController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (env.IsDevelopment())
            {
                return Redirect("/swagger");
            }
            return Redirect("/swagger");
            //return Content("Hello,Api");
        }
    }
}
