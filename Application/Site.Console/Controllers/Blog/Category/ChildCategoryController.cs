using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Blog.Category
{
    [AllowAnonymous]
    public class ChildCategoryController : WebBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}