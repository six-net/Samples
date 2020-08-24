using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EZNEW.ViewModel.Sys;
using Site.Console.Util;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Console.Controllers
{
    [AllowAnonymous]
    public class AccountController : WebBaseController
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var loginResult = IdentityManager.Login(loginViewModel);
            return Json(loginResult);
        }

        public IActionResult LoginOut()
        {
            IdentityManager.LoginOut();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// ÑéÖ¤Âë
        /// </summary>
        /// <returns></returns>
        public IActionResult VCode()
        {
            byte[] byteValues = VerificationCodeHelper.RefreshLoginCode();
            return File(byteValues, "image/jpeg");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
