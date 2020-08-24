using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Blog
{
    [AllowAnonymous]
    public class CategoryController : WebBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}