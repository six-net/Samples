using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Http;
using EZNEW.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Console.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ApiBaseController : BaseController<long>
    {
    }
}
